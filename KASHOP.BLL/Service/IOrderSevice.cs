using KASHOP.DAL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IOrderSevice
    {
        Task<List<OrderResponse>> GetUserOrder(string userId);//for user
    }
}
