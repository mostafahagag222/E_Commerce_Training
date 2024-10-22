using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Entities.Configurations
{
    public class PaymentLogConfiguration : IEntityTypeConfiguration<PaymentLog>
    {
        public void Configure(EntityTypeBuilder<PaymentLog> builder)
        {
            builder
                .HasOne(pl => pl.Payment)
                .WithOne(p => p.PaymentLog)
                .HasForeignKey<PaymentLog>(pl => pl.PaymentId);
        }
    }
}
