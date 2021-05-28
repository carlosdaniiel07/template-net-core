﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TemplateNetCore.Domain.Entities;

namespace TemplateNetCore.Repository.EF.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.HasQueryFilter(entity => entity.DeletedAt == null);

            builder.Property(entity => entity.Id)
                .HasColumnName("id");

            builder.Property(entity => entity.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(entity => entity.DeletedAt)
                .HasColumnName("deleted_at");
        }
    }
}
