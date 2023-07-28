using HustlerzOasiz.Web.ViewModels;
using HustlerzOasiz.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HustlerzOasiz.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {}
        [AllowAnonymous]
        public IActionResult Start()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Start(string password)
        {
            // Replace "YourValidPassword" with the actual valid password.
            string validPassword = "Tarantula22";

            if (password == validPassword)
            {
                // Password is correct, redirect to the main page.
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}