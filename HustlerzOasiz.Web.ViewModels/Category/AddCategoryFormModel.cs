using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Category;

namespace HustlerzOasiz.Web.ViewModels.Category
{
    public class AddCategoryFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;


        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
