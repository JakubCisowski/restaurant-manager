using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Restaurants
{
    public class Menu : Entity
    {
        public Menu()
        {
        }

        public Guid RestaurantId { get; private set; }
        public virtual Restaurant Restaurant { get; private set; } = default!;
        public virtual ICollection<Dish> Dishes { get; private set; } = default!;

        public void AddRestautant(Restaurant restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
