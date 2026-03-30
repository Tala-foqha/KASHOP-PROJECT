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
    public interface IBrandService
    {
        public Task CreateBrandAsync(BrandRequest request);
        public Task<List<BrandResponse>> GetAllBranAsync();
        public Task<BrandResponse?> GetBrand(Expression<Func<Brand,bool>> filtter);
        public Task<bool> DeleteBrand(int id);

    }
}
