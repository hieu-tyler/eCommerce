using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class LanguageConfigurations : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");

            builder.Property(x => x.Id).IsRequired().IsUnicode(false).HasMaxLength(5);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        }
    }
}
