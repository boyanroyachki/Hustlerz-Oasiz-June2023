using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Category;

namespace MarauderzOasiz.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Jobs = new HashSet<Job>();
        }

        [Key]
        public int Id { get; set; }  //id is integer, for  Postman guesing whould not be harmful

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; } //description is optional

        public virtual ICollection<Job> Jobs { get; set; }  //jobs can be empty
    }
}
