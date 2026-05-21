using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IOrderSevice
    {
        Task<List<OrderResponse>> GetUserOrders(string userId);//for user
        Task<OrderDetailsResponse?> GetUserOrder(string userId, int orderId);
        Task<bool>CancelOrder(string userId,int orderId);
         Task<List<OrderResponse>> GetAllOrders(OrderStatusEnum status);
        Task<bool> ChangeOrderStatus(int orderId, ChangeOrderStatusRequest request);
    }
}
