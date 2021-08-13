using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Orders)
                   .WithOne(x => x.Customer);
        }
    }
}
