using ECommerce.ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ECommerce.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(p => p.OriginalPrice)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(p => p.Stock).IsRequired().HasDefaultValue(0);
        
            builder.Property(p => p.ViewCount).IsRequired().HasDefaultValue(0);

        }
    }
}
