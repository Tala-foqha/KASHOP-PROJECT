using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CheckoutCotroller : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        public CheckoutCotroller(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult>Payment([FromBody]CheckoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res =await _checkoutService.processCheckout(userId, request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> success([FromQuery]string sessionId) {
            var result =await _checkoutService.HandleSuccess(sessionId);
            return Ok(result);
        }
    }
}
