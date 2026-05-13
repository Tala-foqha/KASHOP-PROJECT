using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace KASHOP.PL.Extentions
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;//0-9
                options.Password.RequireLowercase = true;//a-z
                options.Password.RequireUppercase = true;//A-Z
                
                options.Password.RequireNonAlphanumeric = true;// romoz !@#$%
                options.Password.RequiredLength = 10;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            return Services;
        }
    }
}
