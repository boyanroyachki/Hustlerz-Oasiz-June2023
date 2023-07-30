using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Infrastructure;
using HustlerzOasiz.Web.ViewModels.Job;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Common.NotificationMessagesConstants;

namespace HustlerzOasiz.Web.Controllers
{

	public class JobController : BaseController
    {
        private readonly IJobService jobService;
        private readonly ICategoryService categoryService;
        private readonly IContractorService contractorService;

        //not a problem to inject all services
        public JobController(IJobService jobService, ICategoryService categoryService, IContractorService contractorService)
        {
            this.jobService = jobService;
            this.categoryService = categoryService;
            this.contractorService = contractorService;
        }

        
        public async Task<IActionResult> Index()
        {
           return View();
        }  //done and working

        [HttpGet]
        public async Task<IActionResult> PublishAjob()
        {
            bool isContractor =
                await contractorService.ContractorExistsByUserIdAsync(User.GetId()!);
            if (!isContractor)
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
        }   //done and working
        [HttpPost]
        public async Task<IActionResult> PublishAJob(PublishAJobViewModel model)
        {

            bool isContractor =
                await contractorService.ContractorExistsByUserIdAsync(User.GetId()!);
            if (!isContractor)
            {
                TempData[ErrorMessage] = "You must be a Contractor to post Jobs!";

                return RedirectToAction("Join", "Contractor");
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
                //return BadRequest();

            }
        }   //done and working

        //try
        public  IActionResult BrowseJobs(int? categoryId = null)
        {
            var jobs = this.jobService.GetJobsByCategory(categoryId);

            return this.View(jobs);
        }  //need to add contractor 

        //public IActionResult Detail(Guid id)
        //{
        //    var wantedJob = jobService.GetByIdAsync(id.ToString());
        //    if (wantedJob != null)
        //    {
        //        return this.View(wantedJob);
        //    }
        //    return BadRequest(); //not done
        //}  
        

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool jobExists = await this.jobService.JobExistsByIdAsync(id);

            if (!jobExists)
            {
                this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
                return RedirectToAction("BrowseJobs", "Job");
            }
            bool isUserContractor = await this.contractorService.ContractorExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserContractor)
            {
                this.TempData[ErrorMessage] = "You must be a contractor in order to edit jobs!";
                return RedirectToAction("Join", "Contractor");
            }
            
            string contractorId = await this.contractorService.GetContractorIdByUserIdAsync(this.User.GetId()!);
            bool isContractorOwner = await this.jobService.IsContractorWithIdOwnerOfJobAsync(id, contractorId);

            if (!isContractorOwner)
            {
                this.TempData[ErrorMessage] = "Cannot edit jobs you dont own!";
                return RedirectToAction("MyJobs", "Job");
            }

            try
            {
				JobFormModel jobFormModel = await this.jobService.GetJobForEditAsync(id);

				jobFormModel.Categories = await this.categoryService.GetCategoriesAsync();

				return View(jobFormModel);
			}
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error accured!";

                return this.RedirectToAction("BrowseJobs", "Job");
            }

            
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, JobFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetCategoriesAsync();

                return this.View(model);
            }

            try
            {
                await this.jobService.EditJobByJobIdAndJobFormMode(id, model);
            }
            catch (Exception)
            {

                this.ModelState.AddModelError(string.Empty, "Unexpected error accured while trying to edit the Job!");
                model.Categories =await this.categoryService.GetCategoriesAsync();
               
                return this.View(model);
            }
            return this.RedirectToAction("Details", "Job", new {id});
        }


		




	}
}
