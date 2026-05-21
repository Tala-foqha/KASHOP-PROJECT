using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using Stripe.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class OrderService : IOrderSevice
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderResponse>> GetUserOrders(string userId)
        {
            var orders = await _orderRepository.GetAllAsync(
                filter: o => o.UserId == userId,
                includes: new[]
                {
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}",
                $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}.{nameof(Product.Translations)}"

                }

                );
            return orders.Adapt<List<OrderResponse>>();
        }
        public async Task<OrderDetailsResponse?>GetUserOrder(string userId,int orderId)
        {
            var order = await _orderRepository.Getone(
                filter:o=>o.UserId==userId && o.Id== orderId,
                includes: new[]
                {
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}",
                $"{nameof(Order.OrderItems)}.{nameof(OrderItem.product)}.{nameof(Product.Translations)}"

                }

                );
            if (order == null) return null;
            return order.Adapt<OrderDetailsResponse>();
        }

        public async Task<bool> CancelOrder(string userId, int orderId)
        {
            var order = await _orderRepository.Getone(
                 filter: o => o.UserId == userId && o.Id == orderId
                );
            if (order is null) return false;
            //لسا م توافق ع هاد الطلب 
            if (order.OrderStatus != OrderStatusEnum.Pending)
            {
                return false; 
            }
            order.OrderStatus = OrderStatusEnum.Cancelled;
            return await _orderRepository.UpdateAsync( order );
        }

        public async Task<List<OrderResponse>> GetAllOrders(OrderStatusEnum status)
        {
            var orders = await _orderRepository.GetAllAsync(
                filter:o=>o.OrderStatus == status

                );
            return orders.Adapt<List<OrderResponse>>();
        }

        public async Task<bool> ChangeOrderStatus(int orderId, ChangeOrderStatusRequest request)
        {
            var order = await _orderRepository.Getone(
                filter:o=>o.Id==orderId
                );
            if (order.OrderStatus == OrderStatusEnum.Cancelled || order.OrderStatus == OrderStatusEnum.Delivered) return false;
            if ((int)request.Status != (int)order.OrderStatus + 1) return false;
            order.OrderStatus = request.Status;
            return await _orderRepository.UpdateAsync(order);
                }
    }
}