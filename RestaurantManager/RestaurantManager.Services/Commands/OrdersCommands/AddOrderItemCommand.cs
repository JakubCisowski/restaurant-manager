using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.Commands.Orders
{
    public class AddOrderItemCommand
    {
        public Guid OrderId { get; set; }
        public Guid DishId { get; set; }
        public ICollection<Guid> ExtraIngredientIds { get; set; }

    }
}
