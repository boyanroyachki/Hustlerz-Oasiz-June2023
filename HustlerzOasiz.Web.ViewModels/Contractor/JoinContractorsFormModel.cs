using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Contractor;

namespace HustlerzOasiz.Web.ViewModels.Contractor
{
    public class JoinContractorsFormModel
    {
        [Required]
        [MaxLength(UsernameMaxLength)]
        [MinLength(UsernameMinLength)]
        [Display(Name = "Choose a Username:")]
        public string Username { get; set; } = null!;



        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        [Phone]   //General atribute for phone numbers, not custom
        [Display(Name = "Contact Info:")]
        public string PhoneNumber { get; set; } = null!;
    }
}
