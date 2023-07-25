using HustlerzOasiz.Services.Data;
using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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
            bool isJoined = await this.contractorService.ContractorExistsById(userId);
            if (isJoined)
            {
                return BadRequest();
            }
            return View();
        }
    }
}
