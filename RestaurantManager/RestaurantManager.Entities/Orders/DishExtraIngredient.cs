using System;

namespace RestaurantManager.Entities.Orders
{
    public class DishExtraIngredient : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Guid OrderItemId { get; private set; }
        public OrderItem OrderItem { get; private set; } = default!;
    }
}