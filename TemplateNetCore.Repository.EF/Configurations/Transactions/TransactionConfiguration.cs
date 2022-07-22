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

            builder.ToTable("Transactions");

            builder.Property(entity => entity.Description)
                .HasMaxLength(100);

            builder.Property(entity => entity.Value)
                .HasColumnType("decimal(19,2)");

            builder.Property(entity => entity.Status);

            builder.Property(entity => entity.UserId);

            builder.HasOne(entity => entity.User)
                .WithMany()
                .HasForeignKey(entity => entity.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Transactions_Users");
        }
    }
}
