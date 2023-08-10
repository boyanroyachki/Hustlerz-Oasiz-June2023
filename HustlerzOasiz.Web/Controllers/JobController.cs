using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Infrastructure;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            //saw this online
            this.jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            this.contractorService = contractorService ?? throw new ArgumentNullException(nameof(contractorService));
        }

        
        public async Task<IActionResult> Index()
        {
           return View();
        }  //done and working


        

        [HttpGet]
        public async Task<IActionResult> PublishAjob()
        {
            string? userId = this.User.GetId();
            if (userId == null || !await contractorService.ContractorExistsByUserIdAsync(userId)) //added
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return RedirectToAction("Index", "Home");
                
            }
        }   //done and working
        [HttpPost]
        public async Task<IActionResult> PublishAJob(PublishAJobViewModel model)
        {

            string? userId = this.User.GetId();
            if (userId == null || !await contractorService.ContractorExistsByUserIdAsync(userId))  //added
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

                if (contractorId == null)  //added
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred. Contractor ID is null.");
                    model.Categories = await categoryService.GetCategoriesAsync();
                    return View(model);
                }


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
        [AllowAnonymous]
        public  IActionResult BrowseJobs(int? categoryId = null)   //done
        {
            var jobs = this.jobService.GetJobsByCategory(categoryId);
            return this.View(jobs);
        }  //need to add contractor 

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            //var wantedJob = jobService.GetByIdAsync(id.ToString());
            //wantedJob.Category = await this.categoryService.GetCategoryByIdAsync(wantedJob.CategoryId);
            //wantedJob.Contractor = await this.contractorService.GetContractorByUserIdAsync(User.GetId()!);
            //if (wantedJob != null)
            //{
            //    return this.View(wantedJob);
            //}
            //return BadRequest(); //not done

            var wantedJob =  jobService.GetByIdAsync(id.ToString());
            if (wantedJob != null)
            {
                string? userId = this.User.GetId();
                if (userId != null)
                {
                    wantedJob.Category = await this.categoryService.GetCategoryByIdAsync(wantedJob.CategoryId);
                    wantedJob.Contractor = await this.contractorService.GetContractorByUserIdAsync(userId);
                    return this.View(wantedJob);
                }
            }
            return BadRequest();
        }

        [HttpPost]  //not done
		public async Task<IActionResult> Detail(Guid id, Job model)
        {
			if (!ModelState.IsValid)
			{
				return this.View(model);
			}

			try
			{
                await this.contractorService.AdoptJobByUserIdAndJobIdAsync(this.User.GetId()!, id.ToString());
                return this.RedirectToAction("Index", "Home");

            }
			catch (Exception)
			{

				this.ModelState.AddModelError(string.Empty, "Unexpected error accured while trying to adopt the Job!");

				return this.View(model);
			}
			
		}

		[HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool jobExists = await this.jobService.JobExistsByIdAsync(id);

            if (!jobExists)
            {
                this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
                return RedirectToAction("BrowseJobs", "Job");
            }
            bool isUserAdmin = this.User.IsAdmin();
            if (!isUserAdmin)
            {
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
            this.TempData[SuccessMessage] = "Job was edited successfully";
            if (this.User.IsAdmin())
            {
                this.TempData[WarningMessage] = "The Administrator applied changes on the application!";
            }
            return this.RedirectToAction("Detail", "Job", new {id});
        }

        //done!
        public IActionResult MyJobs(int? categoryId = null)
        {
            try
            {
                string userId = this.User.GetId();
                var jobs = this.jobService.GetJobsByCategory(categoryId);
                var result = jobs.Where(x => x.ExecutorId.ToString() == userId);
                

                return View(result);
			}
            catch (Exception)
            {

				this.TempData[ErrorMessage] = "You have not adopted any jobs!";
				return RedirectToAction("BrowseJobs", "Job");
			}
        } 

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
			bool jobExists = await this.jobService.JobExistsByIdAsync(id);

			if (!jobExists)
			{
				this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
				return RedirectToAction("BrowseJobs", "Job");
			}
			bool isUserContractor = await this.contractorService.ContractorExistsByUserIdAsync(this.User.GetId()!);

            bool isUserAdmin = this.User.IsAdmin();
            if (!isUserAdmin)
            {
                if (!isUserContractor)
                {
                    this.TempData[ErrorMessage] = "You must be a contractor in order to delete jobs!";
                    return RedirectToAction("Join", "Contractor");
                }

                string contractorId = await this.contractorService.GetContractorIdByUserIdAsync(this.User.GetId()!);
                bool isContractorOwner = await this.jobService.IsContractorWithIdOwnerOfJobAsync(id, contractorId);

                if (!isContractorOwner)
                {
                    this.TempData[ErrorMessage] = "Cannot delete jobs you dont own!";
                    return RedirectToAction("MyJobs", "Job");
                }
            }

			

            try
            {
                JobDeleteViewModel model = await this.jobService.GetJobForDeleteByIdAsync(id);

                return this.View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error accured while trying to delete job!";
                return this.RedirectToAction("Index", "Home");
            }
		}
        [HttpPost]
        public async Task<IActionResult> Delete(string id, JobDeleteViewModel model)
        {

            bool jobExists = await this.jobService.JobExistsByIdAsync(id);

            if (!jobExists)
            {
                this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
                return RedirectToAction("BrowseJobs", "Job");
            }
            bool isUserContractor = await this.contractorService.ContractorExistsByUserIdAsync(this.User.GetId()!);

            bool isUserAdmin = this.User.IsAdmin();
            if (!isUserAdmin)
            {
                if (!isUserContractor)
                {
                    this.TempData[ErrorMessage] = "You must be a contractor in order to delete jobs!";
                    return RedirectToAction("Join", "Contractor");
                }

                string contractorId = await this.contractorService.GetContractorIdByUserIdAsync(this.User.GetId()!);
                bool isContractorOwner = await this.jobService.IsContractorWithIdOwnerOfJobAsync(id, contractorId);

                if (!isContractorOwner)
                {
                    this.TempData[ErrorMessage] = "Cannot delete jobs you dont own!";
                    return RedirectToAction("MyJobs", "Job");
                }
            }



            try
            {
                await this.jobService.DeleteJobByIdAsync(id);

                this.TempData[SuccessMessage] = "You successesfully deleted the selected job!";

                if (this.User.IsAdmin())
                {
                    this.TempData[WarningMessage] = "The Administrator applied changes in the application!";
                }
                return this.RedirectToAction("BrowseJobs", "Job");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error accured while trying to delete job!";
                return this.RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Adopt(string id)
        {
            bool jobExist = await this.jobService.JobExistsByIdAsync(id);
            if (!jobExist)
            {
                this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
                return this.RedirectToAction("BrowseJobs", "Job");
            }

            //bool isJobOwnedByCurrentUser = await this.jobService.IsContractorWithIdOwnerOfJobAsync(this.User.GetId()!, id);
            //if (isJobOwnedByCurrentUser)
            //{
            //    this.TempData[ErrorMessage] = "You cannot adopt jobs you have published!";
            //    return this.RedirectToAction("BrowseJobs", "Job");
            //}

            bool isJobAdopted = await this.jobService.IsJobAdoptedByIdAsync(id);
            bool isJobAdoptedByCurrentUser = await this.jobService
                .IsJobAdoptedByUserWithIdAsync(id, this.User.GetId()!);

            if (isJobAdopted)
            {
                if (isJobAdoptedByCurrentUser)
                {
                    this.TempData[ErrorMessage] = "This job is already adopted by you!";
                    return this.RedirectToAction("MyJobs", "Job");
                }
                this.TempData[ErrorMessage] = "This job is already adopted by another executor!";
                return this.RedirectToAction("BrowseJobs", "Job");
            }


            try
            {
                string userId = this.User.GetId()!;
                await this.jobService.AdoptJobByIdAsync(id, userId);
                this.TempData[SuccessMessage] = "Successesfully adopted job!";
                return this.RedirectToAction("MyJobs", "Job");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error accured while trying to adopt the job.";
                return this.RedirectToAction("BrowseJobs", "Job");
            }
        }
        //quit action to do

        [HttpGet]
        public async Task<IActionResult> Quit(string id) 
        {

            bool jobExist = await this.jobService.JobExistsByIdAsync(id);
            if (!jobExist)
            {
                this.TempData[ErrorMessage] = "Job with the given ID does not exist!";
                return this.RedirectToAction("MyJobs", "Job");
            }

            bool isJobAdopted = await this.jobService.IsJobAdoptedByUserWithIdAsync(id, this.User.GetId()!);
            if (!isJobAdopted)
            {
                this.TempData[ErrorMessage] = "This job is not adopted by you!";
                return this.RedirectToAction("MyJobs", "Job");
            }

            try
            {
                string userId = this.User.GetId()!;
                await this.jobService.QuitJobByIdAsync(id, userId);

                this.TempData[WarningMessage] = "Job is successesfully quited!";

                return this.RedirectToAction("MyJobs", "Job");
            }
            catch (Exception)
            {

                this.TempData[ErrorMessage] = "Unexpected error accured while trying to quit job!";
                return this.RedirectToAction("MyJobs", "Job");
            }
        }

        
    }
}
