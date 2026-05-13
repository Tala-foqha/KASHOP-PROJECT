using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class OrderService : IOrderSevice
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderResponse>> GetUserOrder(string userId)
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
    }
}