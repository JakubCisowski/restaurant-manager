using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Restaurants
{
    public class Dish : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }

    }
}
