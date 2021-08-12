using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.SqlContext.Configuration.Restaurants
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Menu)
                   .WithMany(x => x.Dishes)
                   .HasForeignKey(x => x.MenuId);

            builder
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Dishes);

        }
    }
}
