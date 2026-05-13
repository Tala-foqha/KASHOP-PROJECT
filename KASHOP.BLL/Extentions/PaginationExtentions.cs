using KASHOP.DAL.Dto.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Extentions
{
    public static class PaginationExtentions
    {
        //IQurable لسا بدي اكملهم م خلصت وبرجع الجواب مرة وحدة
        public static async Task <PaginationResponse<T>>ToPaginationAsync<T>(this IQueryable<T>query,int page,int limit)
        {
            var totalCount = await query.CountAsync();
            // جبلي حسب الليمت هيك التيك بتجيب عدد معين بس هيك كل مرة رح تجيب مثلا اول خمسة بس احنا بدنا
            //حسب الصفحات
            var data = await query.Skip((page -1)*limit).Take(limit).ToListAsync();
            return new PaginationResponse<T>
            {
                Data = data,
                TotalCount = totalCount,
                Page=page,
                Limit=limit
            };
        }
    }
}
