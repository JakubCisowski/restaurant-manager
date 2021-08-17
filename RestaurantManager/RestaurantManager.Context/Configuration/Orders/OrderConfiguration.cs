using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.ShippingAddress)
                .WithOne(x => x.Order)
                .HasForeignKey<ShippingAddress>(x => x.OrderId);
        }
    }
}
