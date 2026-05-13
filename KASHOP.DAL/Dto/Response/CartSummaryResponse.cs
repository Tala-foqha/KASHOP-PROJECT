using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; }
    }
}
