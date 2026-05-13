using KASHOP.BLL.Service;
using KASHOP.DAL;
using KASHOP.DAL.Dto.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices, IStringLocalizer<SharedResources> localizer)
        {
            _cartServices = cartServices;
            _localizer = localizer;
        }
        [HttpPost("")]
        [Authorize]
        //بفك التوكن وبشفره ومنه بنجيب الاي دي
        public async Task<IActionResult> AddToCart(AddToCartRequest request)

        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var result=  await _cartServices.AddToCart(request, UserId);
            if (!result) return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _cartServices.GetCart(UserId);
            return Ok(new { data = items });
        }
        [HttpDelete ("{productId}")]       
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute]int productId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var removed = await _cartServices.RemoveItem(productId,UserId);
            if (!removed) return BadRequest();
            return Ok(new {  });
        }
        [HttpPatch("{productId}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuantity([FromRoute]int productId, [FromBody]UpdateCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var update = await _cartServices.UpdateQuantity(productId,  request.Count, UserId);
            if (!update) return BadRequest();
            return Ok();
        }


    }
}
