using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce1DB_V01.Entities.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Baskets)
                .HasForeignKey(c => c.UserID);
        }
    }
}
