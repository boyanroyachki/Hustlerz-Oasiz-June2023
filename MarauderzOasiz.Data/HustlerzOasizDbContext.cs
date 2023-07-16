using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace HustlerzOasiz.Web.Data
{
    public class HustlerzOasizDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public HustlerzOasizDbContext(DbContextOptions<HustlerzOasizDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<Contractor> Contractors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(HustlerzOasizDbContext)) ??
                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(builder);
        }
    }
}