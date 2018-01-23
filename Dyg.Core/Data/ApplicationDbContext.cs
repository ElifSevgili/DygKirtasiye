using Dyg.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dyg.Core.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories{ get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<BrandCategory>()
              .HasKey(t => new { t.BrandId, t.CategoryId });

            builder.Entity<BrandCategory>()
                .HasOne(pt => pt.Brand)
                .WithMany(p => p.BrandCategories)
                .HasForeignKey(pt => pt.BrandId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BrandCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(t => t.BrandCategories)
                .HasForeignKey(pt => pt.CategoryId).OnDelete(DeleteBehavior.Cascade);

                 builder.Entity<User>()
                .HasAlternateKey(c => c.Email)
                .HasName("AlternateKey_UserEmail");

        }

    }
}
