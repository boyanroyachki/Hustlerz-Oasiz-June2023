using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HustlerzOasiz.Services.Data
{
    public class JobService : IJobService
    {
        private readonly HustlerzOasizDbContext data;

        public JobService(HustlerzOasizDbContext data) => this.data = data;

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await data.Jobs.ToArrayAsync();
        }

        public Job GetById(string id)
        {
            var wantedJob =  data.Jobs.FirstOrDefault(j => j.Id.ToString() == id);

            return wantedJob;
        }

        public async Task<IEnumerable<JobsIndexViewModel>> LatestJobsAsync()
        {
           IEnumerable<JobsIndexViewModel> latestJobs = await this.data.Jobs
				.OrderByDescending(j => j.DatePosted)
                                 .Take(3)
                .Select(j => new JobsIndexViewModel()
                {
                    Id = j.Id.ToString(),
                    Title = j.Title,
                    ImageUrl = j.ImageURLs
                }).ToListAsync();

            return latestJobs;
        }

        public async Task PublishJobAsync(PublishAJobViewModel model, string contractorId)
        {
            Job job = new Job()
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                Price = (decimal)model.Price,
                DatePosted = DateTime.Now,
                CategoryId = model.CategoryId,
                ContractorId = Guid.Parse(contractorId),
                Deadline = model.Deadline,
                ImageURLs = model.ImageURLs
            };

            await data.Jobs.AddAsync(job);
            await data.SaveChangesAsync();
        }


        //try
        public List<Job> GetJobsByCategory(int? categoryId = null)
        {
            IQueryable<Job> jobsQuery = this.data.Jobs;

            if (categoryId.HasValue)
            {
                jobsQuery = jobsQuery.Where(job => job.CategoryId == categoryId.Value);
            }

            return jobsQuery.ToList();
        }


    }
}
