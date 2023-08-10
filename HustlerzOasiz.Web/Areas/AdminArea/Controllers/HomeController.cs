using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Areas.AdminArea.ViewModels;
using HustlerzOasiz.Web.Infrastructure;
using HustlerzOasiz.Web.ViewModels.Category;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Common.NotificationMessagesConstants;

namespace HustlerzOasiz.Web.Areas.AdminArea.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        public HomeController(ICategoryService categoryService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            //Overkill

            try
            {
                if (!this.User.IsAdmin())
                {
                    this.TempData[ErrorMessage] = "Only Admin can add Categories!";
                    this.RedirectToAction("Index", "Home");
                }
                return View();

            }
            catch (Exception)
            {

                this.TempData[ErrorMessage] = "Unexpected error accured!";
                return RedirectToAction("Index");   
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await this.categoryService.CreateCategoryAsync(model);
                this.TempData[SuccessMessage] = "Successesfully added a Category!";
                this.TempData[WarningMessage] = "The Administrator has made changes on the application!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error accured!";
                return View(model);
            }

            

            return RedirectToAction("Index"); 
        }



    }
}
