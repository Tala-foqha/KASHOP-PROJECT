using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
   public interface IGenericRepository<T> where T:class
    {
        Task<List<T>> GetAllAsync(String[]? includes = null);
      
            Task<T> CreateAsync(T entity);
        Task<T?> Getone(Expression<Func<T, bool>> filter, String[]? includes = null);
        Task<bool> DeleteAsync(T entity);
    }
}
