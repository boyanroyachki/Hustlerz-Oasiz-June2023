using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Jobs;
using MarauderzOasiz.Data.Models;
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

        public List<Job> GetAllJobs()
        {
            throw new NotImplementedException();
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

       




    }
}
