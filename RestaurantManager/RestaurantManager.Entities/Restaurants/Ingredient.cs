using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Restaurants
{
    public class Ingredient : Entity
    {
        public string Name { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }
        public virtual ICollection<Dish> Dishes { get; private set; } = new List<Dish>();

        public Ingredient(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public void SetName(string name)
        {
            Name = name;

        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }
    }
}
