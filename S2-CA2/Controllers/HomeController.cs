using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S2_CA2.Models;

namespace S2_CA2.Controllers
{
    public class HomeController(SignInManager<IdentityUser> signInManager) : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Books");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}