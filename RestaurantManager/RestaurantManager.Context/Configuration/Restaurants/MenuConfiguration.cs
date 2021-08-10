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
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Restaurant)
                .WithOne(x => x.Menu);

            builder
                .HasMany(x => x.Dishes)
                .WithOne(x => x.Menu);
        }
    }
}
