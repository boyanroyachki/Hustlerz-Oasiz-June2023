using MarauderzOasiz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HustlerzOasiz.Data.Models
{
    public class Archive
    {
        public Archive()
        {
            Id = Guid.NewGuid();
            ArchivedJobsOfUser = new HashSet<Job>();
        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ICollection<Job> ArchivedJobsOfUser { get; set; }
    }
}
