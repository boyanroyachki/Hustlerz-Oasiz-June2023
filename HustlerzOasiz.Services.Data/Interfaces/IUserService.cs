using MarauderzOasiz.Data.Models;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserContractorByUserIdAsync(string userId);

        Task<IEnumerable<Job>> GetUsersAdoptedJobsByUserIdAsync(string userId);
    }

}
