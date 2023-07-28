﻿using HustlerzOasiz.Web.ViewModels.Contractor;
using MarauderzOasiz.Data.Models;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface IContractorService
    {

        Task<bool> ContractorExistsByUserIdAsync(string  userId);

        Task<bool> ContractorExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> UserHasAdoptedJobsByUserIdAsync(string userId);

        //Task<ICollection<Job>> GetUsersAdoptedJobsByIdAsync(string userId);

        Task<bool> ContractorExistsByUsernameAsync(string userName);

        //ICollection<Job> GetContractorsAdoptedJobsByContractorId(string contractorId);

        Task Create(string userId, JoinContractorsFormModel model);

        Task<Contractor> GetContractorByUserIdAsync(string userId);

        Task<string> GetContractorIdByUserIdAsync(string userId);


    }
}
