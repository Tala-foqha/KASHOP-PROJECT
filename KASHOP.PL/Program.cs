using KASHOP.BLL.Mapping;
using KASHOP.BLL.Service;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.DAL.Utils;
using KASHOP.PL.Extentions;
using KASHOP.PL.images;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

namespace KASHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -------------------- Services --------------------

            builder.Services.AddControllers();

            // DB
            builder.Services.AddDatabaseService(builder.Configuration);

            // Localization
            builder.Services.AddLocalizationServices();
            // DI
            builder.Services.AddAplicationServices(builder.Configuration);




            builder.Services.AddHttpContextAccessor();

            // Identity
            builder.Services.AddIdentityServices();

            // JWT
            builder.Services.AddJWTAuthntication(builder.Configuration);

            // 🔴 لازم تكون قبل Build
            builder.Services.AddAuthorization();

            // -------------------- Build --------------------
            MapsterConfig.MapsterConfigRegister();
            builder.Services.AddOpenApi();
            var app = builder.Build();

            // 🔴 لإظهار الخطأ الحقيقي
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.MapOpenApi();
           
            // Localization
            app.UseRequestLocalization(
                app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            // Seed Data (محمي من الكراش)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();

                foreach (var seeder in seeders)
                {
                    try
                    {
                        await seeder.DataSeed();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Seeding Error: {ex.Message}");
                    }
                }
            }

            // Pipeline
            app.UseHttpsRedirection();

            app.UseAuthentication();   // ✔ مهم جداً
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}