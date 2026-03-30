using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        //دائما اول وحد الي بترجع كلشي هي الاندكس
        public async Task<IActionResult>Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new
            {
                data = products
            }
                
           );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _productService.GetProduct(p=>p.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                data = product
            }

           );
        }


        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            await _productService.CreateProduct(request);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
           var deleted= await _productService.DeleteProduct(id);
            if (!deleted) return BadRequest();
            return Ok();
        }

    }
}
