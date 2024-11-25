using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce1DB_V01.Entities.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder
                .HasOne(ci => ci.Product)
                .WithMany(p => p.BasketItems)
                .HasForeignKey(ci => ci.ProductID);
            builder
                .HasOne(ci => ci.Basket)
                .WithMany(c => c.BasketItems)
                .HasForeignKey(ci => ci.BasketID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
