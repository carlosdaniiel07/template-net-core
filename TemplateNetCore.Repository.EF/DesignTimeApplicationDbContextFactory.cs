using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TemplateNetCore.Repository.EF
{
    public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseNpgsql("Server=192.168.99.100;Port=5432;Database=template_net_core;User Id=postgres;Password=postgres;");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
