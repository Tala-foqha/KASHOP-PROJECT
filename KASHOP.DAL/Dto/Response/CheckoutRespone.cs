using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class CheckoutRespone
    {
        public int OrderId { get; set; }
        public string StripeUrl { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
