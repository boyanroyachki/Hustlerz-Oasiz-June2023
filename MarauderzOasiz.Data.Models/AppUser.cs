using Microsoft.AspNetCore.Identity;

namespace MarauderzOasiz.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
           this.AdoptedJobs = new HashSet<Job>();
        }
    public virtual ICollection<Job> AdoptedJobs { get; set; }
    }
}
