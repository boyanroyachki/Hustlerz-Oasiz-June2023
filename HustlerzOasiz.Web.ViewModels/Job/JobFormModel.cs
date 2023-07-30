using MarauderzOasiz.Data.Models;
using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Job;

namespace HustlerzOasiz.Web.ViewModels.Job
{
	public class JobFormModel
	{
		public JobFormModel() { 

			Status = JobStatus.Active.ToString();
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
		public decimal Price { get; set; }

		[Required]                   //not done yet
		public string Status { get; set; } = null!;

		[Required]
		public DateTime DatePosted { get; set; }

		//Info about the Category of the Contract	
		[Required]
		public int CategoryId { get; set; }

		//Info about who posted the Contract
		[Required]
		public Guid ContractorId { get; set; }

		//Deadline is not required, for the Contract can be left for adoption for an unlimited time
		public DateTime? Deadline { get; set; }

		public string? ImageURLs { get; set; }

	

	}
}
