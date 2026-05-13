using KASHOP.BLL.Service;
using KASHOP.DAL.Repository;
using KASHOP.DAL.Utils;
using KASHOP.PL.images;
using Stripe;

namespace KASHOP.PL.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services,IConfiguration Configuration)
        {
          Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();
          Services.AddScoped<IAuthenticationUsers, AuthenticationUsers>();
            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartServices, CartServices>();
            Services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];


            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ICheckoutService, BLL.Service.CheckoutService>();
          Services.AddTransient<IEmailSender, EmailSender>();
           Services.AddTransient<IFileService, BLL.Service.FileService>();
       Services.AddTransient<IProductService, BLL.Service.ProductService>();
          Services.AddTransient<IProductRepository, ProductRepository>();
            Services.AddTransient<IOrderRepository, OrderRepository>();

            Services.AddTransient<IBrandRepository, BrandRepository>();
          Services.AddTransient<IBrandService, BrandService>();
            return Services;
        }
    }
}
