using MarauderzOasiz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HustlerzOasiz.Web.ViewModels.User
{
    public class UserViewModel
    {
        [Required]
        public AppUser UsersInfo { get; set; } = null!;

        public string? ContractorId { get; set; } 

        public int NumberOfPublishedJobs { get; set; }
        public int NumberOfCurrentlyAdoptedJobs { get; set; }
        public int NumberOfAllAdoptedJobsEver { get;}
    }
}
