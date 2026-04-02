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
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request, string lang = "en")
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);
            if (request.translations == null || !request.translations.Any() || request.translations.Any(t => t == null))
            {
                throw new ArgumentException("Translations cannot be null or contain null items");
            }
            return category.BuildAdapter().AddParameters("lang", lang).AdaptToType<CategoryResponse>();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category =await _categoryRepository.Getone(c=>c.Id==id);
            if (category == null)
            {
              return false; 

            }return await _categoryRepository.DeleteAsync(category);

            
        }

        public async Task<List<CategoryResponse>> GetAllCategories(String lang="en")
        {
            var categories = await _categoryRepository.GetAllAsync(
                c=>c.Status==EntityStatus.Active,
                new string[] {nameof(Category.translations),
                nameof(Category.CreateBy)});
            var response = categories.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<CategoryResponse>>();
            return response;
        }
        public async Task<CategoryResponse?> GetCategory(Expression<Func<Category,bool>>filter)
        {
            var category = await _categoryRepository.Getone(filter, new string[] { nameof(Category.translations) });

            return category.Adapt<CategoryResponse>();
        }
    }
}