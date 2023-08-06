using HustlerzOasiz.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HustlerzOasiz.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController() { }

        //[AllowAnonymous]
        
        //public IActionResult Start()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //     return View();
        //    }
        //}
        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Start(string password)
        //{
        //    // Replace "YourValidPassword" with the actual valid password.
        //    string validPassword = "Tarantula22";

        //    if (password == validPassword)
        //    {
        //        // Password is correct, redirect to the main page.
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}                              

        [AllowAnonymous]
        public IActionResult Index()  //done and working
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 404 || statusCode == 400)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }
            return View("Error");
        }
    }
}