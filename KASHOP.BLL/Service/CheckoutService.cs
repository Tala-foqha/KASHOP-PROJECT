using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stripe.Checkout;
using Stripe.FinancialConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace KASHOP.BLL.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartServices _cartServices;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSender _emailSender;
        public CheckoutService(ICartRepository cartRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, ICartServices cartService, IProductRepository productRepository, IEmailSender emailSender)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _cartServices = cartService;

            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task<CheckoutRespone> HandleSuccess(string sessionId)
        {
            var order = await _orderRepository.Getone(o => o.StripeSessionId == sessionId,
                includes: new[]
                {
                     nameof(Order.OrderItems),
                     $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}",
                                          $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}.{nameof(Product.Translations)}"

                });
            order.OrderStatus = OrderStatusEnum.Paid;
            await _orderRepository.UpdateAsync(order);
            await _cartServices.ClearCart(order.UserId);
            var user = await _userManager.FindByIdAsync(order.UserId);
            var lowStockProduct = await _productRepository.DecreaseQuantityAsync(order.OrderItems) ;
            await _emailSender.SendEmailAsync(user.Email, "order confirm", "<h2> your order has been placed successfully </h2>");
            
            foreach (var item in lowStockProduct)
            {
                if (lowStockProduct!=null)
                {
                    await _emailSender.SendEmailAsync($"foqhat835@gmail.com", "low stock alert", $"<h2>product {item.Translations.FirstOrDefault(l => l.Language == "en")} current quantity :{item.Quantity}</h2>");
                }
            }

            return new CheckoutRespone
            {
                Success = true,
                OrderId = order.Id
            };
        }

        public async Task<CheckoutRespone> processCheckout(string UserId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetAllAsync(u => u.UserId == UserId,
                new[]
                {
              nameof(Cart.Product),
 $"{nameof(Cart.Product)}.{nameof(DAL.Models.Product.Translations)}"
                });
            if (!cartItems.Any())
            {
                return new CheckoutRespone
                {
                    Success = false,
                    Error = "Cart is empty"
                };
            }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return new CheckoutRespone
                {
                    Success = false,
                    Error = "User not found"
                };
            }
            var city = request.City ?? user.City;
            if (string.IsNullOrEmpty(city))
            {
                return new CheckoutRespone
                {
                    Success = false,
                    Error = "City is required"
                };
            }
            var street = request.Street ?? user.Street;
            if (string.IsNullOrEmpty(street))
            {
                return new CheckoutRespone
                {
                    Success = false,
                    Error = "Street is required"
                };
            }
            var phoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new CheckoutRespone
                {
                    Success = false,
                    Error = "Phone number is required"
                };
            }

            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Product.Quantity)
                {
                    //لازم احكي للمستخدم انه ما في كمية كافية من المنتجواكتب اسم المنتج
                    return new CheckoutRespone
                    {
                        Success = false,
                        Error = $"Not enough quantity for product"
                    };
                             }
               

            }
            Order order = new Order
            {
                OrderStatus=OrderStatusEnum.Pending,
                UserId = UserId,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = request.PaymentMethod,
                City = city,
                Street = street,
                PhoneNumber = phoneNumber,
                AmountPaid = cartItems.Sum(c => c.Quantity * c.Product.Price),
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Count,
                    Unitprice = c.Product.Price,
                    TotalPrice = c.Quantity * c.Product.Price

                }).ToList()
            };
            await _orderRepository.CreateAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {

                return new CheckoutRespone
                {
                    Success = true,

                };
            }
            foreach (var item in cartItems)
            {
                if (item.Count <= 0)
                    return new CheckoutRespone { Success = false, Error = "Invalid quantity" };

                if (item.Product.Price <= 0)
                    return new CheckoutRespone { Success = false, Error = "Invalid product price" };
            }
            foreach (var item in cartItems)
            {
                Console.WriteLine($"Product: {item.ProductId}");
                Console.WriteLine($"Price: {item.Product.Price}");
                Console.WriteLine($"Count: {item.Count}");
            }
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    //اذا تمت عمليه الدفع بنجاح لازم احول اليوزر ع مكان معين واذا فشلت نفس الشغله بس ع مكان تاني
                    SuccessUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/CheckoutCotroller/success?sessionId={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Checkouts/cancel",
                    //المنتجات يلي بالسله بدي اعطيهم لسترايب عشان يعرهم بالصفحه
                    LineItems = new List<SessionLineItemOptions>()

                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(item.Product.Price * 100), // Stripe expects amount in cents,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Translations.FirstOrDefault(x => x.Language == "en")?.Name ?? "Product"
                            }
                        },
                        Quantity = item.Count
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
                order.StripeSessionId = session.Id; //لتحديث الطلب
                await _orderRepository.UpdateAsync(order);
                return new CheckoutRespone
                {
                    Success = true,
                    StripeUrl = session.Url//رابط صفحه الدفع يلي راح يفتح ع سترايب
                };
            }

            return new CheckoutRespone
            {
                Success = false,
                Error = "Invalid payment method"
            };
        }
    }
    }
