using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
