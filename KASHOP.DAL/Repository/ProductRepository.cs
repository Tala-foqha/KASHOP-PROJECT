using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class ProductRepository : GenericRepositry<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Product>?> DecreaseQuantityAsync(List<OrderItem>orderItems)
            //the loop inside 
            //هيك يعني بنستدعي الفنكشن من السيرفس مرة وحدة بس مش زي قبل كنا نيتدعيه جوا فور لوب بالسيرفس
        {
            //list contain id of all product
            var productIds = orderItems.Select(i => i.ProductId).ToList();
            var products =  GetQureable(p=>productIds.Contains(p.Id));
            foreach (var product in products)
            {
                var item = orderItems.FirstOrDefault(p => p.ProductId == product.Id);
                product.Quantity -= item.Quantity;


            }
            await UpdateRangAsync(products);
            return products.Where(p=>p.Quantity < 5).ToList();
        }
    }
}
