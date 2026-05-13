using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    [PrimaryKey(nameof(ProductId),nameof(OrderId))]
    public class OrderItem
    {
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int OrderId { get; set; }
        public decimal Unitprice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
