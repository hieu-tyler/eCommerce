using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Configurations
{
    public class AppRoleConfigurations : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
        }
    }
}
