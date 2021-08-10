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
    public class DishExtraIngredientConfiguration : IEntityTypeConfiguration<DishExtraIngredients>
    {
        public void Configure(EntityTypeBuilder<DishExtraIngredients> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.OrderItem)
                .WithMany(x => x.DishExtraIngredients)
                .HasForeignKey(x => x.OrderItemId);
        }
    }
}
