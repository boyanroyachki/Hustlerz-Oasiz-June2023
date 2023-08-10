using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Category;


namespace HustlerzOasiz.Web.Areas.AdminArea.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
