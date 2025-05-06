using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Configurations
{
    public class ProductInCategoryConfigurations : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(t => new { t.CategoryId, t.ProductId });

            builder.ToTable("ProductInCategories");

            builder.HasOne(t => t.Product)
                .WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(t => t.Category)
                .WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
