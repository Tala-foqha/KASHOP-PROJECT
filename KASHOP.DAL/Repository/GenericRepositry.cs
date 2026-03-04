using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class GenericRepositry<T>:IGenericRepository<T> where T:class
    {


        private readonly ApplicationDbContext _context;
        public GenericRepositry(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
          await  _context.Set<T>().AddAsync(entity);
         await   _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
       var affected= await  _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<List<T>> GetAllAsync(String[]?includes=null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
            //    var response = _context.Adapt<List<CategoryResponse>>();
            //}
        }
        public async Task<T?> Getone(Expression<Func<T,bool>>  filter,String[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(filter);
        }
    }
  // ( c=>checked.name)
}

