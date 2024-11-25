using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce1DB_V01.Entities.Configurations
{
    public class PaymentLogConfiguration : IEntityTypeConfiguration<PaymentLog>
    {
        public void Configure(EntityTypeBuilder<PaymentLog> builder)
        {
            builder
                .HasOne(pl => pl.Payment)
                .WithMany(p => p.PaymentLogs)
                .HasForeignKey(pl => pl.PaymentId);
        }
    }
}
