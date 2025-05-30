﻿using ECommerce.ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ECommerce.Data.Configurations
{
    public class ContactConfigurations : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            
            builder.Property(x => x.PhoneNumber).HasMaxLength(200).IsRequired();
            
            builder.Property(x => x.Message).IsRequired();

        }
    }
}
