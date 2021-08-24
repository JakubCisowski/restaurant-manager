using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Restaurants
{
    public class Dish : Entity
    {
        public Dish(Guid id, string name, decimal basePrice, string description)
        {
            Id = id;
            Name = name;
            BasePrice = basePrice;
            Description = description;
        }

        public string Name { get; private set; }
        public decimal BasePrice { get; private set; }
        public string Description { get; private set; }
        public bool IsAvailable { get; private set; }
        public virtual ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        public Guid MenuId { get; private set; }
        public virtual Menu Menu { get; private set; } = default!;

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetBasePrice(decimal basePrice)
        {
            BasePrice = basePrice;

        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetMenu(Menu menu)
        {
            Menu = menu;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
