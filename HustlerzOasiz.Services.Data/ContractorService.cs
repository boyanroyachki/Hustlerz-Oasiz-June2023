using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Contractor;
using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HustlerzOasiz.Services.Data
{
    public class ContractorService : IContractorService
    {
        private readonly HustlerzOasizDbContext data;

        public ContractorService(HustlerzOasizDbContext data) => this.data = data;

        //returns TRUE if a Contractor with the given ID exists, else FALSE
        public async Task<bool> ContractorExistsByUserIdAsync(string userId)
        {
            return await data.Contractors.AnyAsync(c => c.UserId.ToString() == userId);
        }

        //returns TRUE if a Contractor with the given PhoneNumber exists in the db context, else FALSE
        public async Task<bool> ContractorExistsByPhoneNumberAsync(string phoneNumber)
        {
           return await data.Contractors.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

		public async Task<bool> ContractorExistsByUsernameAsync(string userName)
		{
			return await data.Contractors.AnyAsync(c => c.Username == userName);
		}

		//public async Task<ICollection<Job>> GetUsersAdoptedJobsByIdAsync(string userId)
		//{
		//    var user = data.Users.Find(userId);
		//    if (user != null) 
		//    {
		//        return  user.AdoptedJobs.ToArray();
		//    }
		//    return null;
		//}
		public async Task<ICollection<Job>> GetContractorsAdoptedJobsByContractorIdAsync(string contractorId)
		{
			// First, find the Contractor by the provided Id.
			Contractor contractor = await this.data.Contractors.FirstOrDefaultAsync(c => c.Id.ToString() == contractorId);

			if (contractor == null)
			{
				// If no Contractor was found, return null or handle it as you see fit.
				return null;
			}

			// Find the User linked to this Contractor.
			AppUser user = await this.data.Users.FirstOrDefaultAsync(u => u.Id.ToString() == contractor.UserId.ToString());

			if (user == null)
			{
				// If no User was found, return null or handle it as you see fit.
				return null;
			}

			// If everything is fine, return the AdoptedJobs of the linked User.
			return user.AdoptedJobs.ToList();
		}


		public async Task<bool> UserHasAdoptedJobsByUserIdAsync(string userId)
        {
            AppUser? user = await data
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return false;
            }

            return user.AdoptedJobs.Any();

        }

        public Task<ICollection<Job>> GetUsersAdoptedJobsByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

		public async Task Create(string userId, JoinContractorsFormModel model)
		{
			Contractor contractor = new Contractor()
			{
				Username = model.Username,
				PhoneNumber = model.PhoneNumber,
				UserId = Guid.Parse(userId)
			};
			await data.AddAsync(contractor);
			await data.SaveChangesAsync();
		}
    }
}
