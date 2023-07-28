using HustlerzOasiz.Services.Data;
using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Infrastructure;
using HustlerzOasiz.Web.ViewModels.Category;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Common.NotificationMessagesConstants;

namespace HustlerzOasiz.Web.Controllers
{
     
    public class JobController : BaseController
    {
        private readonly IJobService jobService;
        private readonly ICategoryService categoryService;
        private readonly IContractorService contractorService;

        public JobController(IJobService jobService, ICategoryService categoryService, IContractorService contractorService)
        {
            this.jobService = jobService;
            this.categoryService = categoryService;
            this.contractorService = contractorService;
        }

        
        public async Task<IActionResult> Index()
        {
           return View();
        }

        [HttpGet]
        public async Task<IActionResult> PublishAjob()
        {
            bool isAgent =
                await contractorService.ContractorExistsByUserIdAsync(User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must be a Contractor to post Jobs!";

                return RedirectToAction("Join", "Contractor");
            }

            try
            {
                PublishAJobViewModel formModel = new PublishAJobViewModel()
                {
                    Categories = await categoryService.GetCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception )
            {
                //return GeneralError();
                return RedirectToAction("Index", "Home");
                
            }
        }
        [HttpPost]
        public async Task<IActionResult> PublishAJob(PublishAJobViewModel model)
        {
            bool isContractor =
                await contractorService.ContractorExistsByUserIdAsync(User.GetId()!);
            if (!isContractor)
            {
                TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Agent");
            }

            bool categoryExists =
                await categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetCategoriesAsync();

                return View(model);
            }

            try
            {
                string? contractorId =
                    await contractorService.GetContractorIdByUserIdAsync(User.GetId()!);

                
                    await jobService.PublishJobAsync(model, contractorId!);

                TempData[SuccessMessage] = "Job was published.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add the new Job! Please try again later or contact administrator!");
                model.Categories = await categoryService.GetCategoriesAsync();

                return View(model);
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> PublishAJob()
        //{
        //    bool isContractor = await contractorService.ContractorExistsByUsernameAsync(this.User.GetId());
        //    if (!isContractor)
        //    {
        //        this.TempData[ErrorMessage] = "Only Contractors can post Jobs.";
        //        return RedirectToAction("Join", "Contractor");
        //    }

        //    var categories = await this.categoryService.GetCategoriesAsync();
        //    var model = new PublishAJobViewModel
        //    {
        //        Categories = categories.Select(c => new ChooseACategoryFormModel
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Description = c.Description
        //        })
        //    };

        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> PublishAJob(PublishAJobViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.Categories = await this.categoryService.GetCategoriesAsync();
        //        return View(model);
        //    }

        //    var job = new Job
        //    {
        //        Title = model.Title,
        //        Location = model.Location,
        //        Details = model.Details,
        //        Price = model.Price,
        //        CategoryId = model.CategoryId,
        //        ContractorId = model.ContractorId,
        //        Deadline = model.Deadline,
        //        ImageURLs = model.ImageURLs
        //    };

        //    await /*this.jobService.Create(job);*/

        //    return RedirectToAction("Index", "Home");
        //}


        //[HttpPost]
        //public ActionResult<Job> PostProduct(Job job)
        //{
        //    job = jobService.CreateJob(job.Title, job.Details, job.Price, job.);

        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}




        //try
        public  IActionResult BrowseJobs(int? categoryId = null)
        {
            var jobs = this.jobService.GetJobsByCategory(categoryId);

            return this.View(jobs);
        }
        



    }
}
