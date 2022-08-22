using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tutorial.Models;

namespace Tutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    Id = 1,
                    Name = "Cap",
                    Price = 10,
                    Description = "Color: Stylish Black",
                    Slug="Cap",
                    CategoryId=1

                });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Fashion",
                    Slug="Fashion",
                    Sorting = 100
                });*/

            modelBuilder.Entity<Page>().HasData(
                        new Page
                        { 
                            Id=1,
                            Title = "Home",
                            Slug = "home",
                            Content = "home page",
                            Sorting = 0
                        },
                        new Page
                        {
                            Id = 2,
                            Title = "About Us",
                            Slug = "about-us",
                            Content = "about us page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Id = 3,
                            Title = "Services",
                            Slug = "services",
                            Content = "services page",
                            Sorting = 100
                        },
                        new Page
                        {   Id = 4,
                            Title = "Contact",
                            Slug = "contact",
                            Content = "contact page",
                            Sorting = 100
                        }
               );
        }
    }
}