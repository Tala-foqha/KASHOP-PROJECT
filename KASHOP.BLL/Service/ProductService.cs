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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        public  ProductService (IProductRepository productRepository,IFileService fileService) {
            _fileService = fileService;
            _productRepository = productRepository;
        }
        public async Task CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }
            await _productRepository.CreateAsync(product);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(
                new string[]
            {
                nameof(Product.Translations),
                nameof(Product.CreateBy)
            }
            );
            return products.Adapt<List<ProductResponse>>();
        }
        public async Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>>filtter)
        {
            var product = await _productRepository.Getone(filtter, new string[]
            {
                nameof(Product.Translations),
                nameof(Product.CreateBy)
            });
            if (product == null) return null;

            return product.Adapt<ProductResponse>();

        }
        public async Task<bool>DeleteProduct(int id)
        {
            var product = await _productRepository.Getone(c => c.Id==id);
            if (product == null) return false;
             _fileService.Delete(product.MainImage);
            return await _productRepository.DeleteAsync(product);





        }

    }
}
