﻿using HustlerzOasiz.Services.Data.Interfaces;
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


        //Methods:

        //returns TRUE if a Contractor with the given ID exists, else FALSE
        public async Task<bool> ContractorExistsByUserIdAsync(string userId)
        {
            return await data.Contractors.AnyAsync(c => c.UserId.ToString() == userId);
        }

        //

        //returns TRUE if a Contractor with the given PhoneNumber exists in the db context, else FALSE
        public async Task<bool> ContractorExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await data.Contractors.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        //

        public async Task<bool> ContractorExistsByUsernameAsync(string userName)
        {
            return await data.Contractors.AnyAsync(c => c.Username == userName);
        }

        //

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

        //


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

        //

        //public async Task<AppUser> GetUserByUserIdAsync(string userId)
        //{
        //    AppUser user = await this.data.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        //    return user;
        //}

        //

        public async Task<Contractor> GetContractorByUserIdAsync(string userId)
        {
            if (await ContractorExistsByUserIdAsync(userId))
            {
                var contractor = await this.data.Contractors.FirstOrDefaultAsync(c => c.UserId.ToString() == userId);
                return contractor;
            }
            return null;
        }

        //

        public async Task<string> GetContractorIdByUserIdAsync(string userId)
        {
            Contractor? contractor = await data.Contractors.FirstOrDefaultAsync(c => c.UserId.ToString() == userId);
            if (contractor == null)
            {
                return null;
            }

            return contractor.Id.ToString();
        }

        //

        public Contractor GetContractorByContractorIdAsync(string contractorId)
        {
            Contractor? contractor = data.Contractors.FirstOrDefault(c => c.Id.ToString() == contractorId);
            return contractor;
        }

        //

        public async Task AdoptJobByUserIdAndJobIdAsync(string userId, string jobId)
        {
            AppUser? user = await this.data.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
            Job? job = await this.data.Jobs.FirstOrDefaultAsync(x => x.Id.ToString() == jobId);
            user.AdoptedJobs.Add(job);

            await this.data.SaveChangesAsync();
        }

        //

        public bool ContractorExistsByUserAsync(AppUser user)
        {
            string userId = user.Id.ToString();
            return data.Contractors.Any(c => c.UserId.ToString() == userId);

        }

        //

        public async Task<IEnumerable<Job>> GetContractorsOwnedJobsByUserIdAsync(string userId)
        {
            Contractor contractor = await this.data.Contractors.FirstAsync(x => x.UserId.ToString() == userId);

            string contravtorId = contractor.Id.ToString();

            Job[] contractorsOwnedJobs = await this.data.Jobs
                .Where(x => x.ContractorId.ToString() == contravtorId)
                .ToArrayAsync();

            return contractorsOwnedJobs;
        }
    }
}
