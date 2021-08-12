using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.SqlContext.Configuration.Restaurants
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Dishes)
                .WithOne(x => x.Menu);
        }
    }
}
