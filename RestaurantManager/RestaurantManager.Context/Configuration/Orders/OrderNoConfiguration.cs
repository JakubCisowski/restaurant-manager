using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class OrderNoConfiguration : IEntityTypeConfiguration<OrderNumber>
    {
        public void Configure(EntityTypeBuilder<OrderNumber> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
