using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using static HustlerzOasiz.Common.EntityValidationConstants.Contractor;

namespace MarauderzOasiz.Data.Models
{
    public class Contractor
    {
        public Contractor()
        {
            Id = Guid.NewGuid();
            OwnedJobs = new HashSet<Job>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(UsernameMinLength)]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(PhoneNumberMinLength)]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        public virtual AppUser User { get; set; } = null!;


        public virtual ICollection<Job> OwnedJobs { get; set; }
    }
}

