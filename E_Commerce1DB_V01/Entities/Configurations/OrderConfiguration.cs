using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce1DB_V01.Entities.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            builder
                .HasOne(o => o.Basket)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.BasketId)
                .OnDelete(DeleteBehavior.SetNull);
            builder
                .HasOne(o => o.ShippingMethod)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShippingMethodId);
        }
    }
}
