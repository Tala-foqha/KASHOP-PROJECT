using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KASHOP.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public IHttpContextAccessor _httpContextAccessor;
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<BrandTranslation> BrandTranslations { get; set; }



        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            builder.Entity<Category>().HasOne(p => p.CreateBy).WithMany().
                HasForeignKey(p=>p.CreateById).OnDelete(DeleteBehavior.Restrict);
            //ممنوع نحذف اليوزر اذا كان  تابع اله كاتيجوري او برودكت
            builder.Entity<Category>().HasOne(p => p.UpdateBy).WithMany().
                HasForeignKey(p => p.UpdateById).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>().HasOne(p => p.CreateBy).WithMany().
               HasForeignKey(p => p.CreateById).OnDelete(DeleteBehavior.Restrict);
            //ممنوع نحذف اليوزر اذا كان  تابع اله كاتيجوري او برودكت
            builder.Entity<Product>().HasOne(p => p.UpdateBy).WithMany().
                HasForeignKey(p => p.UpdateById).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Brand>().HasOne(p => p.CreateBy).WithMany().
             HasForeignKey(p => p.CreateById).OnDelete(DeleteBehavior.Restrict);
            //ممنوع نحذف اليوزر اذا كان  تابع اله كاتيجوري او برودكت
            builder.Entity<Brand>().HasOne(p => p.UpdateBy).WithMany().
                HasForeignKey(p => p.UpdateById).OnDelete(DeleteBehavior.Restrict);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // هو عمل حفظ ماشي؟ انا بدي تجيب التغيرات الي الها علاقة بهاد الكلاس وورثت منه
                ///// ممكن يكونوا اكثر من وحدة فبدنا نمشي عليهم بفور اي عملة حفظ رح تيجي هون
                var entries = ChangeTracker.Entries<AuditEntity>();
                var CurrentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (var entity in entries)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Property(x => x.CreateById).CurrentValue = CurrentUserId;
                        entity.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;
                    }

                    if (entity.State == EntityState.Modified)
                    {
                        entity.Property(x => x.UpdateById).CurrentValue = CurrentUserId;
                        entity.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                    }
                }
            }

                return base.SaveChangesAsync(cancellationToken);
            }
        

        // ما بدنا الديفولت بنعملها هيك
        // dependancyinjections // اي حدا بدون يعمل اوبجيكت منه اجباري يبعت براميتر
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //optionsBuilder.UseSqlServer("Data Source=db38630.public.databaseasp.net;User ID=db38630;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}

        // ما بدنا اياها هون
        // هيك خلينا الابكليشن دي بي كنتكست هو الي ينادي ع الاوبشنز بدل م الاب ينادي من الافر رايد
        // بكون فيها الكونكشن سترنج الي هي الابشنز بكون فيها اليوز اس كيو ال سيرفر DbContextOptions
    }
}