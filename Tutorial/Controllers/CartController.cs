using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tutorial.Models;
using Tutorial.Data;


namespace Tutorial.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartmodel = new CartViewModel
            {
                CartItems= cart ,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            
            return View(cartmodel);
        }


        /*==================================Add To CART=========================================*/
        public async Task<IActionResult> AddtoCart(int id)
        {
            Products product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("SmallCart");
        }
        
        /*================================== RemoveItem =========================================*/
        public IActionResult Remove(int id)
        { 
         int index = isExist(id);
            var cart = HttpContext.Session.GetJson<List<CartItem>>("cart") ?? new List<CartItem>();
            cart.Remove(cart[index]);
            HttpContext.Session.SetJson("cart", cart);
            return RedirectToAction("Index");
        }


        /*================================== TakeDown =========================================*/
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        /*================================== Discard =========================================*/
        // GET /cart/discard
        public IActionResult Discard()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("cart") ?? new List<CartItem>();
            HttpContext.Session.Remove("cart");
            HttpContext.Session.SetJson("cart", null);
            return RedirectToAction("Index");
        }


        private int isExist(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductId.Equals(id))
                    return i;
            return -1;
        }


    }
}

