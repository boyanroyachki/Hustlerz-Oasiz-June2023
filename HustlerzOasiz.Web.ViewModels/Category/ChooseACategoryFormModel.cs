using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Category;

namespace HustlerzOasiz.Web.ViewModels.Category
{
    public class ChooseACategoryFormModel
    {
        public int Id { get; set; }  //id is integer, for  Postman guesing whould not be harmful

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
