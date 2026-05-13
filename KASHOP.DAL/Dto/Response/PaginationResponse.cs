using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }//1000
        //ممكن يطلع رقم عشري فلازم نقربه 
        // بقرب للاعلى
        public int TotalPage => (int)Math.Ceiling((double)TotalCount / Limit);
        public int Page { get; set; }//1000/50

        public int Limit { get; set; }//500
    }
}
