using RestaurantManager.Entities.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Restaurants
{
    public class Ingredient : Entity
    {
        public string Name { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }
        public virtual ICollection<Dish> Dishes { get; private set; } = default!;
    }
}
