using HustlerzOasiz.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

namespace HustlerzOasiz.Web.Controllers
{
    /// <summary>
    /// only the admin can user this controller
    /// </summary>
    [Authorize(Roles = AdminRoleName)] 
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IContractorService contractorService;
        private readonly IJobService jobService;
        public UserController(IUserService userService, IContractorService contractorService, IJobService jobService)
        {
                this.userService = userService;
                this.contractorService = contractorService;
                this.jobService = jobService;   
        }

        //public async Task<IActionResult> AllUsers()
        //{
        //    var users = await this.userService.GetUsersAsync();
        //    foreach (var user in users)
        //    {
        //        string contractorId = await this.contractorService.GetContractorIdByUserIdAsync(user.Id.ToString());
        //        if (contractorId != null)
        //        {
        //          int numberOfPublishedJobs = await this.jobService.GetNumberOfPublishedJobsByContractorId(contractorId);
        //        }
        //    }
        //}

    }
}
