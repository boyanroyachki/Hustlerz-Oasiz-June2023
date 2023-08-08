using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HustlerzOasiz.Data.Configurations
{
    public class AppUserEntityConfiguration /*: IEntityTypeConfiguration<AppUser>*/
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.UserName).HasDefaultValue("DefaultName");
        }
    }
}
