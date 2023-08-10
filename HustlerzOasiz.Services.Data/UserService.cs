using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.User;
using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace HustlerzOasiz.Services.Data
{
    public class UserService : IUserService
    {
        private readonly HustlerzOasizDbContext data;
        public UserService(HustlerzOasizDbContext data) => this.data = data;




        //returns the collection of jobs, adopted by the asp.net user
        public async Task<IEnumerable<Job>> GetUsersAdoptedJobsByUserIdAsync(string userId)
        {
            AppUser user = await this.data.Users.FirstAsync(x => x.Id.ToString() == userId);
            return  user.AdoptedJobs.ToArray();
        }


        //

        //checks if the user has a contractor instance in the data.Contractors, with a property UserId = the current user's Id
        public async Task<bool> IsUserContractorByUserIdAsync(string userId)
        {  
            return await this.data.Contractors.AnyAsync(x => x.UserId.ToString() == userId);
        }

        //

        public async Task<ICollection<AppUser>> GetUsersAsync()
        {
            AppUser[] users = await this.data.Users.ToArrayAsync();
            return users;
        }

        //
    }
}
