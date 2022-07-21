using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Repository.EF.Configurations.Users
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.HasIndex(entity => entity.Email)
                .IsUnique()
                .HasDatabaseName("Users_UQ_Email");

            builder.Property(entity => entity.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(entity => entity.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(entity => entity.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(entity => entity.Role);
            
            builder.Property(entity => entity.LastLogin);

            builder.Property(entity => entity.IsActive);
        }
    }
}
