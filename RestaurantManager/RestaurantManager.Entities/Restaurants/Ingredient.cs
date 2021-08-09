using RestaurantManager.Entities.Order;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Restaurants
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public virtual ICollection<Dish> Dishes{ get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
