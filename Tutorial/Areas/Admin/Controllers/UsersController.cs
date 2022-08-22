using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tutorial.Data;

namespace Tutorial.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
            private readonly UserManager<IdentityUser> userManager;

            public UsersController(UserManager<IdentityUser> userManager)
            {
                this.userManager = userManager;
            }

            public IActionResult Index()
            {
                return View(userManager.Users);
            }
        
    }
}
