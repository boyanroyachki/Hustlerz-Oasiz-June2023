using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Services.Data.Models.Stats;
using Microsoft.AspNetCore.Mvc;

namespace HustlerzOasiz.WebApi.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatsApiController : ControllerBase
    {
        private readonly IHomeService homeService;
        public StatsApiController(IHomeService homeService) => this.homeService = homeService;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200,Type =  typeof(StatsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                StatsServiceModel stats = await this.homeService.GetStatsFromAppAsync();

                return Ok(stats);
            }
            catch (Exception)
            {
                return this.BadRequest("Unexpected error accured");
            }
        }

    }
}
