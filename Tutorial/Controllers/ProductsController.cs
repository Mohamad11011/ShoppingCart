using Microsoft.AspNetCore.Mvc;
using Tutorial.Models;
using Tutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace Tutorial.Controllers
{
    public class ProductsController : Controller
    {
        public List<Products> products { get; set; }
        public Products product1 { get; set; }
        public int id { get; set; }

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            products = new List<Products>();
            product1 = new Products();
            this._context=context;
        }


        [Route("[Action]")]
        public ActionResult Index(int p = 1)
        {
            int pageSize = 5;
            var products = _context.Products.OrderBy(p => p.Id).ToList()
                .Skip((p - 1) * pageSize).Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);

            return View(products);
        }

        //Details
        public IActionResult Details(int id)
        {
             product1 = _context.Products.Find(id);
            

            ViewBag.Image = product1.Image;
            

            return View(product1);
        }

        //category filtering
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {
            Category category = await _context.Category.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");

            int pageSize = 5;
            var products = _context.Products.OrderByDescending(x => x.Id)
                                            .Where(x => x.CategoryId == category.Id)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Where(x => x.CategoryId == category.Id).Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = categorySlug;

            return View(await products.ToListAsync());
        }
    }
}
