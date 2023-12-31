﻿using AutoMapper;
using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Services.Mapping;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using static HustlerzOasiz.Common.EntityValidationConstants.Job;

namespace HustlerzOasiz.Services.Data
{
    public class JobService : IJobService
	{
		private readonly HustlerzOasizDbContext data;
		//private readonly IMapper mapper;

		public JobService(HustlerzOasizDbContext data/*, IMapper mapper*/)
		{
			this.data = data;
			//this.mapper = mapper;
		}



		//Methods:

		public async Task<IEnumerable<Job>> GetAllJobsAsync()
		{
			return await data.Jobs.ToArrayAsync();
		}

		//

		public Job GetByIdAsync(string id)
		{
			var wantedJob =  data.Jobs.FirstOrDefault(j => j.Id.ToString() == id);
			

			return wantedJob;
		}

		//

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

		//

		public async Task PublishJobAsync(PublishAJobViewModel model, string contractorId)
		{
			//Job job = new Job()
			//{
			//	Title = model.Title,
			//	Location = model.Location,
			//	Details = model.Details,
			//	Price = model.Price,
			//	DatePosted = DateTime.Now,
			//	CategoryId = model.CategoryId,
			//	ContractorId = Guid.Parse(contractorId),
			//	Deadline = model.Deadline,
			//	ImageURLs = model.ImageURLs
			//};
			Job job = AutoMapperConfig.MapperInstance.Map<Job>(model);
			job.ContractorId = Guid.Parse(contractorId);

			await data.Jobs.AddAsync(job);
			await data.SaveChangesAsync();
		}

		//


		//try
		public List<Job> GetJobsByCategory(int? categoryId = null)
		{
			IQueryable<Job> jobsQuery = this.data.Jobs;

            jobsQuery = jobsQuery
                   .Where(job => job.Status == JobStatus.Active.ToString());

            if (categoryId.HasValue)
			{
				jobsQuery = jobsQuery
					.Where(job => job.CategoryId == categoryId.Value);
			}
               
            

			return jobsQuery.ToList();
		}   //not working

		//

		public async Task<bool> JobExistsByIdAsync(string id)
		{
			bool exists = await this.data.Jobs.Where(x => x.Status == "Active").AnyAsync(x => x.Id.ToString() == id);
			return exists;
		}

		//

		public async Task<JobFormModel> GetJobForEditAsync(string jobId)
		{
			var job = await this.data.Jobs.
				Include(j => j.Category)
				.Include(j=> j.Contractor)
				.ThenInclude(j =>j.User)
				.Where(j=> j.Status == "Active")
				.FirstAsync(j=> j.Id.ToString() == jobId);

			return new JobFormModel
			{
			  Status = job.Status,
			  Title = job.Title,
			  Location = job.Location,
			  DatePosted = job.DatePosted,
			  Details = job.Details,
			  Deadline = job.Deadline,
			  CategoryId = job.CategoryId,
			  ImageURLs = job.ImageURLs,
			  Price = job.Price,
			};
		}

		//

		public async Task<bool> IsContractorWithIdOwnerOfJobAsync(string jobId, string contractorId)
		{
			Job job = await data.Jobs.Where(j => j.Status == "Active").FirstAsync(j => j.Id.ToString() == jobId);

			return job.ContractorId.ToString() == contractorId;  //not sure
		}

		//

		public async Task EditJobByJobIdAndJobFormMode(string jobId, JobFormModel model)
		{
			Job job = await this.data.Jobs.Where(j => j.Status == "Active").FirstAsync(j => j.Id.ToString() == jobId);	

			job.Title = model.Title; //
			job.Location = model.Location; //
			job.Details = model.Details; //
			job.Deadline = model.Deadline;
			job.CategoryId = model.CategoryId; //
			job.ImageURLs = model.ImageURLs;
			job.Price = model.Price; //
			job.Status = model.Status;
			job.DatePosted = model.DatePosted;

			await this.data.SaveChangesAsync();
		}

		//

		public IEnumerable<Job> GetUsersJobsByUserIdAsync(string userId)
		{
			Job[] userJobs =  this.data.Jobs.Where(x => x.ExecutorId.ToString() == userId).ToArray();
			return userJobs.Where(x => x.Status == JobStatus.Active.ToString());
		}

		//

		public async Task<JobDeleteViewModel> GetJobForDeleteByIdAsync(string jobId)
		{
			Job? job = await this.data.Jobs.FirstAsync(x => x.Id.ToString() == jobId);

			var jobForDeleteModel = new JobDeleteViewModel()
			{
				Title = job.Title,
				Location = job.Location,
				Details = job.Details,
				DatePosted = job.DatePosted,
			};

			return jobForDeleteModel;
		}

		//

		//Very important
        public async Task DeleteJobByIdAsync(string jobId)
        {
            Job? job = await this.data.Jobs
				.Where(j => j.Status == JobStatus.Active.ToString())
				.FirstAsync(x => x.Id.ToString() == jobId);

			 job.ChangeStatus(JobStatus.Deleted.ToString());
			await this.data.SaveChangesAsync();
        }

		//

        public async Task AdoptJobByIdAsync(string jobId, string userId)
        {
            Job job = await this.data.Jobs.FirstAsync(j => j.Id.ToString() == jobId);
			job.ExecutorId = Guid.Parse(userId);

            AppUser user = await this.data.Users.FirstAsync(u => u.Id.ToString() == userId);
			user.AdoptedJobs.Add(job);

            await this.data.SaveChangesAsync();
        }

		//

        public async Task<bool> IsJobAdoptedByIdAsync(string jobId)
        {
			Job job = await this.data.Jobs.FirstAsync(j => j.Id.ToString() == jobId);

			return job.ExecutorId.HasValue;
        }

		//

        public async Task<bool> IsJobAdoptedByUserWithIdAsync(string jobId, string userId)
        {
            Job job = await this.data.Jobs.Where(j => j.Status == JobStatus.Active.ToString()).FirstAsync(j => j.Id.ToString() == jobId);

			return job.ExecutorId.ToString() == userId && job.ExecutorId.HasValue;
			//in case userId and executor id are both null, the method will return true, so we add HasValue
        }

		//

        //public Task QuitJobByIdAsync(string jobId, string userId)
        //{
        //    throw new NotImplementedException();
        //}

		//

		public async Task QuitJobByIdAsync(string jobId, string userId)
		{
			Job job =  await this.data.Jobs
				.Where(x => x.ExecutorId.ToString() == userId)
				.FirstAsync(x => x.Id.ToString() == jobId);

			job.ExecutorId = null;

			await this.data.SaveChangesAsync();
		}

		//

		//not done
        public async Task<bool> isContractorOwnerOfJobByUserIdAsync(string userId, string jobId)
        {
			var contractor = await this.data.Contractors.FirstAsync(x => x.UserId.ToString() == userId);
			if (contractor == null)
			{
				return false;
			}

			return await this.data.Jobs
				.Where(x => x.ContractorId
				.ToString() == contractor.Id
				.ToString())
				.AnyAsync(x => x.Id.ToString() == jobId);
        }

        

        public async Task<int> GetNumberOfPublishedJobsByContractorId(string contractorId)
        {
            var contractorsJobs = this.data.Jobs.Where(x => x.ContractorId.ToString() == contractorId);
            return contractorsJobs.Count();
        }

        //
    }
}
