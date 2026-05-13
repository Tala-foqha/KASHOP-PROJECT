using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Request
{
    public class AddToCartRequest
    {
        public  int ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
