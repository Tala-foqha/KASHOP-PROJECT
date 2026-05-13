using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface ICartServices
    {
        Task<bool> AddToCart(AddToCartRequest request, string UserId);
        Task<List<CartResponse>> GetCart(string userId);
        Task<bool> UpdateQuantity(int productId, int count,string userId);
        Task<bool> RemoveItem(int productId, string userId);
        Task<bool> ClearCart(string userId);


    }
}
