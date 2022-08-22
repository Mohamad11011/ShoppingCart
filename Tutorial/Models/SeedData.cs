﻿using Tutorial.Data;
using Microsoft.EntityFrameworkCore;


namespace Tutorial.Models
    {
        public class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    if (context.Pages.Any())
                    {
                        return;
                    }

                    context.Pages.AddRange(
                        new Page
                        {
                            Title = "Home",
                            Slug = "home",
                            Content = "home page",
                            Sorting = 0
                        },
                        new Page
                        {
                            Title = "About Us",
                            Slug = "about-us",
                            Content = "about us page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Title = "Services",
                            Slug = "services",
                            Content = "services page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Title = "Contact",
                            Slug = "contact",
                            Content = "contact page",
                            Sorting = 100
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
}
