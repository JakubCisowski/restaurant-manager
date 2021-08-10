using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            
        }
    }
}
