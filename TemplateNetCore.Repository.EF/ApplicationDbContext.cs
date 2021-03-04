using Microsoft.EntityFrameworkCore;

using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Repository.EF.Configurations.Transactions;

namespace TemplateNetCore.Repository.EF
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {

        }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
