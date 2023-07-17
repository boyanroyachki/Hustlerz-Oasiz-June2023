using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HustlerzOasiz.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }
        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();
            Category category;
            category = new Category()
            {
                Id = 1,
                Name = "Ammonition"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Protection"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Special Mission"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Cyber warfare/ Cyber security"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
