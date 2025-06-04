using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Amount).HasPrecision(10, 2);

            builder.Property(x => x.Fee).HasPrecision(10, 2);

            builder.HasOne(x => x.AppUser).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
        }
    }
}
