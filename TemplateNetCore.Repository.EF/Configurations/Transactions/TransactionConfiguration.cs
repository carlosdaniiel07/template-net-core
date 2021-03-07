using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TemplateNetCore.Domain.Entities.Transactions;

namespace TemplateNetCore.Repository.EF.Configurations.Transactions
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.ToTable("transaction");

            builder.Property(entity => entity.Description)
                .HasColumnName("description")
                .HasMaxLength(100);

            builder.Property(entity => entity.Value)
                .HasColumnName("value")
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(entity => entity.TargetKey)
                .HasColumnName("target_key")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(entity => entity.Date)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(entity => entity.Status)
                .HasColumnName("status")
                .IsRequired();
        }
    }
}
