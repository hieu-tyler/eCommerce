using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Configurations
{
    public class CategoryTranslationConfigurations : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SeoAlias).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SeoDescription).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SeoTitle).HasMaxLength(200).IsRequired();
            builder.Property(x => x.LanguageId).IsUnicode(false).HasMaxLength(5).IsRequired();
            
            builder.HasOne(x => x.Language).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.LanguageId);
            builder.HasOne(x => x.Category).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.CategoryId);

        }
    }
}
