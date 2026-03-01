using KASHOP.DAL.Data;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class CategoryRepository : GenericRepositry<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}