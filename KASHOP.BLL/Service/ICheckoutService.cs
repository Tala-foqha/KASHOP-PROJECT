using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface ICheckoutService
    {
        Task<CheckoutRespone> processCheckout(string userId, CheckoutRequest request);
        Task<CheckoutRespone> HandleSuccess(string sessionId);

    }
}
