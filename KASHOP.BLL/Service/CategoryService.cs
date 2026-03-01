using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    //    السيرفس رح يرجع دي تي او 
    {
        private readonly ICategoryRepository _categoryRepository;
        //معناها لما تشوفني بعمل اوبجيكت من الكاتوجيري سيرفس روح اعمل اوبجكت من الجاتيجوري ريبوزتري
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync(new string[] {nameof(Category.translations)});
            return categories.Adapt<List<CategoryResponse>>();
        }
        public async Task<CategoryResponse?> GetCategory(Expression<Func<Category,bool>>filter)
        {
            var category = await _categoryRepository.Getone(filter, new string[] { nameof(Category.translations) });

            return category.Adapt<CategoryResponse>();
        }
    }
}