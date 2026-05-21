using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Models;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
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
            var orders=await _orderSevice.GetUserOrders(userId);
            return Ok(new { date = orders });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderSevice.GetUserOrder(userId,id);
            return Ok(new { date = order });
        }
        [HttpGet("admin")]
        [Authorize()]
        public async Task <IActionResult>GetAllOrders([FromQuery]OrderStatusEnum status = OrderStatusEnum.Pending)
        {
var orders=await _orderSevice.GetAllOrders(status);
            return Ok(orders);
        }
        [HttpPatch("admon/{id}/status")]
        public async Task<IActionResult>ChangeStatus(int id, [FromBody]ChangeOrderStatusRequest request)
        {
            var res = await _orderSevice.ChangeOrderStatus(id, request);
            if (!res) return BadRequest();
            return Ok(res);


        }

        

    }
}
