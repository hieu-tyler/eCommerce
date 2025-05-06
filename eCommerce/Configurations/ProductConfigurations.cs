using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(p => p.Price).IsRequired();
        
            builder.Property(p => p.OriginalPrice).IsRequired();

            builder.Property(p => p.Stock).IsRequired().HasDefaultValue(0);
        
            builder.Property(p => p.ViewCount).IsRequired().HasDefaultValue(0);

        }
    }
}
