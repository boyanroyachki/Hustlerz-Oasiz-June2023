﻿using HustlerzOasiz.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Job;

namespace HustlerzOasiz.Web.ViewModels.Job
{
    public class PublishAJobViewModel
    {
        
        public PublishAJobViewModel()
        {
                
        }
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        [Required]
        [MinLength(DetailsMinLegth)]
        [MaxLength(DetailsMaxLength)]
        public string Details { get; set; } = null!;

        [Required]
        [Display(Name = "Price in USD")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Guid ContractorId { get; set; }

        public DateTime? Deadline { get; set; }  //optional

        public string? ImageURLs { get; set; } //optional

        public IEnumerable<ChooseACategoryFormModel> Categories { get; set; } = new HashSet<ChooseACategoryFormModel>();
    }
}
