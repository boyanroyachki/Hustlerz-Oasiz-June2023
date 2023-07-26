using HustlerzOasiz.Web.ViewModels.Contractor;
using MarauderzOasiz.Data.Models;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface IContractorService
    {

        Task<bool> ContractorExistsByUserIdAsync(string  userId);

        Task<bool> ContractorExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> UserHasAdoptedJobsByUserIdAsync(string userId);

        Task<ICollection<Job>> GetUsersAdoptedJobsByIdAsync(string userId);

        Task<bool> ContractorExistsByUsernameAsync(string userName);

        Task<ICollection<Job>> GetContractorsAdoptedJobsByContractorIdAsync(string contractorId);

        Task Create(string userId, JoinContractorsFormModel model);

	}
}
