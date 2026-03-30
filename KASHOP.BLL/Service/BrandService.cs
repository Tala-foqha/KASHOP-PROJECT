using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.images;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository brandRepository;
        private readonly IFileService _fileService;
        public BrandService(IBrandRepository brandRepository, IFileService fileService)
        {
            this.brandRepository = brandRepository;
            this._fileService = fileService;
        }
        public  async Task CreateBrandAsync(BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
            var logoPath = await _fileService.UploadAsync(request.Logo);
            brand.Logo = logoPath;
            await brandRepository.CreateAsync(brand);

        }

        public async Task<bool> DeleteBrand(int id)
        {
            var brand = await brandRepository.Getone(b => b.Id == id);
            if (brand == null) return false;
            _fileService.Delete(brand.Logo);
           return await brandRepository.DeleteAsync(brand);
        }

        public async Task<List<BrandResponse>> GetAllBranAsync()
        {
            var brands = await brandRepository.GetAllAsync(new string[]
            {
                nameof(Brand.Translations),
                nameof(Brand.CreateBy)
            }
                );
            return  brands.Adapt<List<BrandResponse>>();

        }

        public async Task<BrandResponse?> GetBrand(Expression<Func<Brand,bool>> filtter)
        {
            var brand = await brandRepository.Getone(filtter, new string[] { 
                nameof(Brand.Translations),
                nameof(Brand.CreateBy),
            });
            if (brand == null) return null;
            return brand.Adapt<BrandResponse>();

        }
    }
}
