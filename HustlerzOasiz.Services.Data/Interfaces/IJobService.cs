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
        Task<IEnumerable<JobsIndexViewModel>> LatestJobsAsync();

        List<Job> GetAllJobs();

        Job GetById(int id);

        Job CreateJob(string title, string details, decimal price, int categoryId, string contractorId);

        void EditJob(int id, Job product);

        void EditJobPartially(int id, Job product);

        Job DeleteJob(int id);

    }
}
