using KASHOP.BLL.Service;
using KASHOP.DAL.Data;
using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // new ApplicationDbContext  clr مكون من مكونات الدوت نيت مسؤول عن التعامل مع الذاكرة))ما بدنا اياها رح ننقل مسؤولية انه نخزن بالهيب ل 
      //private readonly  ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;
        //private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService,IStringLocalizer<SharedResources>localizer)
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }
        [HttpPost("")]

        public async Task< IActionResult> Create(CategoryRequest request)
        
        {
            var category =await _categoryService.CreateCategory(request);
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

        [HttpGet("")]
        //الميثود الي بجيب كل الداتا بالعادة بنسمي اندكس
        public async Task <IActionResult> Get() {
            //var category = _context.Categories.Include(c=>c.Translations).ToList();
            var categories =  await _categoryService.GetAllCategories();
            //return only the value
            return Ok(
                new
                {
                    data = categories,

                    _localizer["Success"].Value
                }
                )
                ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
          return Ok( await _categoryService.GetCategory(c => c.Id==id));
        }
    }
}
