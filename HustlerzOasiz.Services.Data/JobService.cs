using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
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

		public Job GetByIdAsync(string id)
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
				Price = model.Price,
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

		public async Task<bool> JobExistsByIdAsync(string id)
		{
			bool exists = await this.data.Jobs.Where(x => x.Status == "Active").AnyAsync(x => x.Id.ToString() == id);
			return exists;
		}

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

		public async Task<bool> IsContractorWithIdOwnerOfJobAsync(string jobId, string contractorId)
		{
			Job job = await data.Jobs.Where(j => j.Status == "Active").FirstAsync(j => j.Id.ToString() == jobId);

			return (job.ContractorId.ToString() != contractorId);  //just for better understanding
		}

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

		public  IEnumerable<Job> GetUsersJobsByUserIdAsync(string userId)
		{
			AppUser user =  this.data.Users.First(u => u.Id.ToString() == userId);

			return  user.AdoptedJobs.ToArray();
		}
	}
}
