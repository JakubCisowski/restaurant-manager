using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Orders
{
    public class DishExtraIngredients : Entity
    {
        public string Name { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }

        public Guid OrderItemId { get; private set; }
        public OrderItem OrderItem { get; private set; } = default!;
    }
}