using KASHOP.BLL.Service;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOrderSevice _orderSevice;
        public OrdersController(IOrderSevice orderSevice, IStringLocalizer<SharedResources> localizer)
        {
            _orderSevice = orderSevice;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetMyOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders=await _orderSevice.GetUserOrder(userId);
            return Ok(new { date = orders });
        }
    }
}
