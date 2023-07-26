using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Infrastructure;
using HustlerzOasiz.Web.ViewModels.Contractor;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Services.Data.ContractorService;
using static HustlerzOasiz.Common.NotificationMessagesConstants;
using MarauderzOasiz.Data.Models;

namespace HustlerzOasiz.Web.Controllers
{
    public class ContractorController : BaseController
    {
        private readonly IContractorService contractorService;

        public ContractorController(IContractorService contractorService)
        {
            this.contractorService = contractorService;
        }

        [HttpGet]
        public async Task<IActionResult> Join()
        {
            string? userId = this.User.GetId();
            bool isJoined = await this.contractorService.ContractorExistsByUserIdAsync(userId);
            if (isJoined)
            {
                TempData[ErrorMessage] = "You are already a CONTRACTOR!";
                return this.RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Join(JoinContractorsFormModel model)
        {
            string? userId = this.User.GetId();
            bool isJoined = await this.contractorService.ContractorExistsByUserIdAsync(userId);

			if (isJoined)
			{
				TempData[ErrorMessage] = "You are already a CONTRACTOR!";
				return this.RedirectToAction("Index", "Home");
			}
			
            bool isPhoneNumberTaken = await contractorService.ContractorExistsByPhoneNumberAsync(model.PhoneNumber);
            bool isUsernameTaken = await contractorService.ContractorExistsByUsernameAsync(model.Username);
            bool userHasAdoptedJobs = await contractorService.UserHasAdoptedJobsByUserIdAsync(userId);


			if (isUsernameTaken)
            {
                ModelState.AddModelError(nameof(model.Username), "Username is taken already!");
            }
            if (isPhoneNumberTaken)
            {
				ModelState.AddModelError(nameof(model.PhoneNumber), "Contractor with the given Phone Number is already created!");
			}

            if (!ModelState.IsValid)
            {
                return View(model);
            }

			try
			{
				await contractorService.Create(userId, model);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] =
					"There was an error while trying to log you as a Contractor. Please, try again! ";

				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("Index", "Home");  //Will add changes if the user is a contractor soon


		}

    }
}
