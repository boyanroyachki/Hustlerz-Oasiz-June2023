using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HustlerzOasiz.Data.Configurations
{
    public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder
               .Property(j => j.IsActive)
               .HasDefaultValue(true);

            builder
                .HasOne(j => j.Category)
                .WithMany(category => category.Jobs)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(j => j.Contractor)
                .WithMany(contractor => contractor.OwnedJobs)
                .HasForeignKey(j => j.ContractorId)
                .OnDelete(DeleteBehavior.Restrict);

            //Due to GDPR, this should be able to be deleted

            //builder.HasOne(j => j.Executor)   
            //    .WithMany(client => client.AdoptedJobs)
            //    .HasForeignKey(j => j.ExecutorId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateJobs());
        }
        private Job[] GenerateJobs()
        {
            ICollection<Job> jobs = new HashSet<Job>();
            Job job;
            job = new Job()
            {
                Title = "Defensive Maneuvers",
                Location = "Middle East",
                Details = "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.",
                Price = 20000m,
                DatePosted = DateTime.Now,
                CategoryId = 2, // Protection
                ContractorId = Guid.Parse("5bb27ea6-15ab-4602-b761-1cd6d4347356"),
                Deadline = DateTime.Now.AddDays(45),
                ExecutorId = Guid.Parse("EAB58257-2186-4F8C-97E3-08DB86032DAB")
            };
            jobs.Add(job);
            new Job
            {
                Title = "Operation Invisible",
                Location = "South Asia",
                Details = "Disrupt enemy communication networks while maintaining a low profile. Requires expertise in cyber warfare and signal jamming.",
                Price = 70000m,
                DatePosted = DateTime.Now,
                CategoryId = 4, // Cyber security
                ContractorId = Guid.Parse("5bb27ea6-15ab-4602-b761-1cd6d4347356"),
                Deadline = DateTime.Now.AddDays(90),
                ExecutorId = Guid.Parse("EAB58257-2186-4F8C-97E3-08DB86032DAB")
            };
            jobs.Add(job);
            new Job
            {
                Title = "Operation Shield",
                Location = "East Europe",
                Details = "Deploy and maintain defensive structures in a high-risk zone. Knowledge in fortification and defensive engineering is essential.",
                Price = 30000m,
                DatePosted = DateTime.Now,
                CategoryId = 2, // Protection
                ContractorId = Guid.Parse("5bb27ea6-15ab-4602-b761-1cd6d4347356"),
                Deadline = null,
                ExecutorId = Guid.Parse("EAB58257-2186-4F8C-97E3-08DB86032DAB")
            };
            jobs.Add(job);

            return jobs.ToArray();
        }
    }
}
