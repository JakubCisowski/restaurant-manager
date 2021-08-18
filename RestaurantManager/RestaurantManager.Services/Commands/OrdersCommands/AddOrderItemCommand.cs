using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.Commands.Orders
{
    public class AddOrderItemCommand
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid DishId { get; set; }
        public string DishComment { get; set; }
        public ICollection<Guid> ExtraIngredientIds { get; set; }

        public AddOrderItemCommand(Guid orderItemId, Guid orderId, Guid dishId, string dishComment, ICollection<Guid> extraIngredientIds)
        {
            Id = orderItemId;
            OrderId = orderId;
            DishId = dishId;
            DishComment = dishComment;
            ExtraIngredientIds = extraIngredientIds;
        }
    }
}
