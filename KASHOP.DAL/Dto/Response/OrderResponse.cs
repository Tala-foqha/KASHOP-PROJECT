using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street {  get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public PaymentMethodEnum Payment {  get; set; }
        public DateTime Orderdate { get; set; }
        public List<OrderItemResponse> orderItems {  get; set; }
    }
}
