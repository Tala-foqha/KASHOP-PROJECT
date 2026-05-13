using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
   public interface GenericRepository<T> where T:class
    {
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, String[]? includes = null);
        IQueryable<T> GetQureable(Expression<Func<T, bool>> filter, String[]? includes = null);

        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<T?> Getone(Expression<Func<T, bool>> filter, String[]? includes = null);
        Task<bool> DeleteAsync(T entity);
        //delete group
        Task<bool> DeleteRangAsync(List<T> entity);
        Task<bool> UpdateRangAsync(List<T> entity);

    }
}
