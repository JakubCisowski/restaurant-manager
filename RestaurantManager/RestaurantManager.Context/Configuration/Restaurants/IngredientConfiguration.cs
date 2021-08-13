using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.SqlContext.Configuration.Restaurants
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Dishes)
                .WithMany(x => x.Ingredients);

            builder.Property(p => p.Price).HasPrecision(18, 2);
        }
    }
}
