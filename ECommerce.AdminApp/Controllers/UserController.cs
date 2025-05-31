using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Logic to create a user
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            return View();
        }
    }
}
