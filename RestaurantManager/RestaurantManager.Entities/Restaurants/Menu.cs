using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Restaurants
{
    public class Menu : Entity
    {
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Dish> Dishes{ get; set; }

    }
}
