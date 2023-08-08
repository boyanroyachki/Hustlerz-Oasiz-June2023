using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.AppUser;

namespace MarauderzOasiz.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
           this.AdoptedJobs = new HashSet<Job>();
        }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        public virtual ICollection<Job> AdoptedJobs { get; set; }
    }
}
