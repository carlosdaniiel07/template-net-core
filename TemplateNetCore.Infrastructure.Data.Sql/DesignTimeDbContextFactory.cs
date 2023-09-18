using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TemplateNetCore.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer("Server=localhost;Database=TemplateNetCore;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=true");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
