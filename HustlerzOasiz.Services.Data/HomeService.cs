using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Services.Data.Models.Stats;
using HustlerzOasiz.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace HustlerzOasiz.Services.Data
{
    public class HomeService : IHomeService
    {
        private readonly HustlerzOasizDbContext data;
        public HomeService(HustlerzOasizDbContext data) => this.data = data;

        public async Task<StatsServiceModel> GetStatsFromAppAsync()
        {
            int totalContractorsCount = await this.data.Contractors.CountAsync();
            int totalJobsCount = await this.data.Jobs.CountAsync();
            int activeJobsCount = await this.data.Jobs.Where(x => x.Status == "Active").CountAsync();
            int totalCategoriesCount = await this.data.Categories.CountAsync();
            int currentlyAdoptedJobs = await this.data.Jobs.Where(x => x.ExecutorId.HasValue).CountAsync();

            StatsServiceModel stats = new StatsServiceModel()
            {
                TotalContractors = totalContractorsCount,
                TotalJobs = totalJobsCount,
                ActiveJobs = activeJobsCount,
                TotalCategories = totalCategoriesCount,
                CurrentlyAdoptedJobs = currentlyAdoptedJobs
            };

            return stats;
        }
    }
}
