using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace KASHOP.PL.Extentions
{
    public static class DatabaseExtentions
    {
        //this IServiceCollection services نوع الي رح يستخدمها والي بعده بكون اول براميتر اذا بدنا نضيف
        public static IServiceCollection AddDatabaseService(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });
            return Services;
        }

    }
}
