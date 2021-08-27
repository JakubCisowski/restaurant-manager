using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class ShippingAdressConfiguration : IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
