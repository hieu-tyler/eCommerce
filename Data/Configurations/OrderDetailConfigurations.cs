using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Data.Configurations
{
    public class OrderDetailConfigurations : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(o => new { o.OrderId, o.ProductId });

            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId);

            builder.HasOne(o => o.Product)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.ProductId);

            builder.Property(c => c.Price).HasColumnType("decimal(10, 2)");

        }
    }
}
