using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.SqlContext.Configuration.Orders
{
    public class OrderNoConfiguration : IEntityTypeConfiguration<OrderNumber>
    {
        public void Configure(EntityTypeBuilder<OrderNumber> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.Property(x => x.Id).HasValueGenerator(DatabaseGeneratedOption.Identity);
        }
    }
}
