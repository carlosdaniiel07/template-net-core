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

            builder.ToTable("user");

            builder.HasIndex(entity => entity.Email)
                .IsUnique();

            builder.Property(entity => entity.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(entity => entity.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(entity => entity.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(entity => entity.Role)
                .HasColumnName("role")
                .IsRequired();

            builder.Property(entity => entity.LastLogin)
                .HasColumnName("last_login");

            builder.Property(entity => entity.IsActive)
                .HasColumnName("is_active")
                .IsRequired();
        }
    }
}
