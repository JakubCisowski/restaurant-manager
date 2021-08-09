using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Orders
{
    public class OrderItem : Entity
    {
        public string DishComment { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public Guid DishId { get; set; }
        public virtual Dish Dish { get; set; }

    }
}
