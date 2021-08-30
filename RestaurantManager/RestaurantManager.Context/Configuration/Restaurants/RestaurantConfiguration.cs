using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.SqlContext.Configuration.Restaurants
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Menu)
                .WithOne(x => x.Restaurant)
                .HasForeignKey<Menu>(x => x.RestaurantId);

            builder
                .HasOne(x => x.Address)
                .WithOne(x => x.Restaurant)
                .HasForeignKey<RestaurantAddress>(x => x.RestaurantId);
        }
    }
}
