﻿using ECommerce.ECommerce.Data.Entities;
using ECommerce.ECommerce.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ECommerce.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(c => c.Status)
                .HasDefaultValue(Status.Active)
                .HasSentinel(Status.Active);
        }
    }
}
