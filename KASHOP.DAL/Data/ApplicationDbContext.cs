using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
      public  DbSet<Category> Categories { get; set; }
      public  DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<String>>().ToTable("UsersRoles");


        }

        //ما بدنا الديفولت بنعملها هيك
        //dependancyinjections//اي حدا بدون يعمل اوبجيكت منه اجباري يبعت براميتر 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //optionsBuilder.UseSqlServer("Data Source=db38630.public.databaseasp.net;User ID=db38630;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}


        //ما بدنا اياها هون 
       //هيك خلينا الابكليشن دي بي كنتكست هو الي ينادي ع الاوبشنز بدل م الاب ينادي من الافر رايد
        
        //بكون فيها الكونكشن سترنج الي هي الابشنز بكون فيها اليوز اس كيو ال سيرفر DbContextOptions
    }
   
}
