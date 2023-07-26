using HustlerzOasiz.Services.Data;
using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HustlerzOasiz.Web.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : BaseController
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        
        public async Task<IActionResult> Index()
        {
            IEnumerable<JobsIndexViewModel> viewModel = await this.jobService.LatestJobsAsync();
            return View(viewModel);
        }


        //[HttpPost]
        //public ActionResult<Job> PostProduct(Job job)
        //{
        //    job = jobService.CreateJob(job.Title, job.Details, job.Price, job.);

        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}

    }
}
