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
        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
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
                p => p.Status == EntityStatus.Active,
                new string[]
            {
                nameof(Product.Translations),
                nameof(Product.CreateBy)
            }

            );
            return products.Adapt<List<ProductResponse>>();
        }
        public async Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>> filtter)
        {
            var product = await _productRepository.Getone(filtter, new string[]
            {
                nameof(Product.Translations),
                nameof(Product.CreateBy)
            });
            if (product == null) return null;

            return product.Adapt<ProductResponse>();

        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _productRepository.Getone(c => c.Id == id);
            if (product == null) return false;
            _fileService.Delete(product.MainImage);
            return await _productRepository.DeleteAsync(product);





        }

        public async Task<bool> UpdateProduct(int Id, ProductUpdateRequest request)

        {
            var product = await _productRepository.Getone(p => p.Id == Id, new string[]
            {
                nameof(Product.Translations)
            });


            if (product == null) return false;

            var oldImage = product.MainImage;
            var prodcut1 = request.Adapt<Product>();//new obj needed in the create
            request.Adapt(product);
            // no create a new obj
            //product.CategoryId = request.CategoryId; // تأكد إنه صحيح
            if (request.Translations != null)
            {
                foreach (var translationRequest in request.Translations)
                {
                    var exisiting = product.Translations.FirstOrDefault(p => p.Language == translationRequest.Language);
                    if (exisiting != null)
                    {
                        if (translationRequest.Name != null)
                        {
                            exisiting.Name = translationRequest.Name;
                        }

                        if (translationRequest.Description != null)
                        {
                            exisiting.Description = translationRequest.Description;
                        }
                    }
                }
            }

                if (request.MainImage != null)
                {
                    //delete the old image
                    _fileService.Delete(oldImage);
                    product.MainImage = await _fileService.UploadAsync(request.MainImage);
                }
                else
                {
                    product.MainImage = oldImage;
                }
                return await _productRepository.UpdateAsync(product);
            }
        public async Task<bool>ToggleStatus(int id)
        {
            var product = await _productRepository.Getone(p => p.Id == id);
            if (product is null) return false;
            product.Status = product.Status == EntityStatus.Active ?
                EntityStatus.InActive : EntityStatus.Active;
            return await _productRepository.UpdateAsync(product);
        }
        }
    }

