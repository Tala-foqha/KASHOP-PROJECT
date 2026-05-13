using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class CartResponse
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public int Count { get; set; }

        public decimal SubTotal =>
            Count * (Price - (Price * Discount) / 100);
    }  }