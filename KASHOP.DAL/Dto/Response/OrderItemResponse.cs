using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class OrderItemResponse
    {
        public int ProductId {  get; set; }
        public string productName {  get; set; }
        public int UnitPrice {  get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
