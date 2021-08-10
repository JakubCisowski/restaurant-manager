using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Restaurants
{
    public class Menu : Entity
    {
        public Menu()
        {
        }

        public Menu(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }
        

        public Guid GetId()
        {
            return Id;
        }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Dish> Dishes{ get; set; }

        public void AddRestautant(Restaurant restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
