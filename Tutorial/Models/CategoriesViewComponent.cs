using Microsoft.AspNetCore.Mvc;
using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Models
{
        public class CategoriesViewComponent : ViewComponent
        {
            private readonly ApplicationDbContext context;

            public CategoriesViewComponent(ApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var categories = await GetCategoriesAsync();
                return View(categories);
            }

            private async Task<List<Category>> GetCategoriesAsync()
            {
            return context.Category.OrderBy(x => x.Sorting).ToList();         
            }
        }
 }

