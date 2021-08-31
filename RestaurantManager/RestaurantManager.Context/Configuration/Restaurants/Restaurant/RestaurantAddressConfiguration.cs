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
    public class RestaurantAddressConfiguration : IEntityTypeConfiguration<RestaurantAddress>
    {
        public void Configure(EntityTypeBuilder<RestaurantAddress> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
