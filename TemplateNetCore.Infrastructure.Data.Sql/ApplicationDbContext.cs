using Microsoft.EntityFrameworkCore;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Infrastructure.Data.Configurations;

namespace TemplateNetCore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
