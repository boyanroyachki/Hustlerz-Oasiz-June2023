using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Data.Configurations
{
    public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
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
        }
    }
}
