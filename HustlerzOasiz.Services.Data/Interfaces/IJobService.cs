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
        Job GetByIdAsync(string id); //
        Task PublishJobAsync(PublishAJobViewModel model, string contractorId); //
        List<Job> GetJobsByCategory(int? categoryId = null); //

        Task<bool> JobExistsByIdAsync(string id); //


        //not done
        Task<JobFormModel> GetJobForEditAsync(string jobId);

        Task<bool> IsContractorWithIdOwnerOfJobAsync(string jobId, string contractorId);

        Task EditJobByJobIdAndJobFormMode(string jobId, JobFormModel model);

        IEnumerable<Job> GetUsersJobsByUserIdAsync(string userId);    

        Task<JobDeleteViewModel> GetJobForDeleteByIdAsync(string jobId);




	}
}
