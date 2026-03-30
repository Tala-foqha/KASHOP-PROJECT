using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBranAsync();
            return Ok(
                new
                {
                    data = brands
                }
                );
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var brand = await _brandService.GetBrand(b=>b.Id==id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(
                new
                {
                    data = brand
                }
                );
        }

        [HttpPost("")]
        [Authorize()]
        public async Task<IActionResult>Create([FromForm]BrandRequest request)
        {
         await _brandService.CreateBrandAsync(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var deleted = await _brandService.DeleteBrand(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(new
            {
                message = "the brand is deleted"
            });
        }

    }
}
