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

        public Job CreateJob(string title, string details, decimal price, int categoryId, string contractorId )
        {
            var job = new Job()
            {
                Title = title,
                Details = details,
                Price = price,
                DatePosted = DateTime.Now,
                CategoryId = categoryId,
                ContractorId = Guid.Parse(contractorId)
            };
            data.Jobs.Add(job);
            data.SaveChanges();
            return job;
        }

        public Job DeleteJob(int id)
        {
            throw new NotImplementedException();
        }

        public void EditJob(int id, Job product)
        {
            throw new NotImplementedException();
        }

        public void EditJobPartially(int id, Job product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await data.Jobs.ToArrayAsync();
        }

        public Job GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobsIndexViewModel>> LatestJobsAsync()
        {
           IEnumerable<JobsIndexViewModel> latestJobs = await this.data.Jobs
                .OrderByDescending(j => j.DatePosted)
                                 .Take(1)
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
