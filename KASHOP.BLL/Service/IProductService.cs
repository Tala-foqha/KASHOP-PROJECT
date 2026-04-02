using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IProductService
    {
        public Task CreateProduct(ProductRequest request);
        public Task<List<ProductResponse>> GetAllProductsAsync();
        public Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>> filtter);
        public Task<bool> DeleteProduct(int id);
        public Task<bool> UpdateProduct(int Id, ProductUpdateRequest request);
        public Task<bool> ToggleStatus(int id);
    }
}
