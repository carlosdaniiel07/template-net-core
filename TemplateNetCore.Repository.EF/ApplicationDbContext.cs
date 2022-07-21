using Microsoft.EntityFrameworkCore;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Repository.EF.Configurations.Transactions;
using TemplateNetCore.Repository.EF.Configurations.Users;

namespace TemplateNetCore.Repository.EF
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
