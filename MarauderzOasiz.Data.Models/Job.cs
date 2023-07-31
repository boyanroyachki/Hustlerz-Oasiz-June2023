using System.ComponentModel.DataAnnotations;
using static HustlerzOasiz.Common.EntityValidationConstants.Job;

namespace MarauderzOasiz.Data.Models
{
	public class Job
    { 
        public Job()
        {
            Id = Guid.NewGuid();
            Status = JobStatus.Active.ToString();
        }

        //Required info about the Contract
        [Key]
        public Guid Id { get; set; }

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

        public virtual Category Category { get; set; } = null!;



        //Info about who posted the Contract
        [Required]
        public Guid ContractorId { get; set; }

        [Required]
        public Contractor Contractor { get; set; } = null!;



        //Deadline is not required, for the Contract can be left for adoption for an unlimited time
        public DateTime? Deadline { get; set; }

        public string? ImageURLs { get; set; }



        //Optional info, for the Contract could be adopted by nobody
        public Guid? ExecutorId { get; set; }

        public virtual AppUser? Executor { get; set; }


        public void ChangeStatus(string status)
        {
            if (status == "Completed")
            {
                Status = JobStatus.Completed.ToString();
            }
            else if (status == "Failed")
            {
                Status = JobStatus.Failed.ToString();
            }
            else if (status == "Quited")
            {
                Status = JobStatus.Quited.ToString();
            }
            else if (status == "Active")
            {
                Status = JobStatus.Active.ToString();
            }
        }

    }
}
