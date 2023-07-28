using HustlerzOasiz.Web.ViewModels.Job;
using MarauderzOasiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobsIndexViewModel>> LatestJobsAsync(); //
        Task<IEnumerable<Job>> GetAllJobsAsync(); //
        Job GetById(string id); //
        Task PublishJobAsync(PublishAJobViewModel model, string contractorId); //
        List<Job> GetJobsByCategory(int? categoryId = null); //

        

    }
}
