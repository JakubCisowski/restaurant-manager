using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class DishExtraIngredientConfiguration : IEntityTypeConfiguration<DishExtraIngredients>
    {
        public void Configure(EntityTypeBuilder<DishExtraIngredients> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.OrderItem)
                .WithMany(x => x.DishExtraIngredients)
                .HasForeignKey(x => x.OrderItemId);

            builder.Property(p => p.Price).HasPrecision(18, 2);
        }
    }
}
