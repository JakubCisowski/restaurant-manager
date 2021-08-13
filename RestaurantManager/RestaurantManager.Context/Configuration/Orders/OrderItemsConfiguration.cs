using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.DishExtraIngredients)
                .WithOne(x => x.OrderItem);

            builder.Property(p => p.DishPrice)
                .HasPrecision(18, 2);

        }
    }
}
