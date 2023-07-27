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
            IEnumerable<JobsIndexViewModel> viewModel = await this.jobService.LatestJobsAsync();
            return View(viewModel);
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

    }
}
