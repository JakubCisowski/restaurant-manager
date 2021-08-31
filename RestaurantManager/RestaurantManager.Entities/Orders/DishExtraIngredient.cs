using System;

namespace RestaurantManager.Entities.Orders
{
    public class DishExtraIngredient : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Guid OrderItemId { get; private set; }
        public virtual OrderItem OrderItem { get; private set; } = default!;

        public DishExtraIngredient(string name, decimal price, Guid orderItemId)
        {
            Name = name;
            Price = price;
            OrderItemId = orderItemId;
        }
    }
}