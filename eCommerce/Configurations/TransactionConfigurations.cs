using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Configurations
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.Amount).HasPrecision(10, 2);

            builder.Property(x => x.Fee).HasPrecision(10, 2);
        }
    }
}
