using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.Commands.Orders
{
    public class AddOrderItemCommand
    {
        public Guid OrderItemId { get; set; }
        public int OrderNo { get; set; }
        public string PhoneNumber { get; set; }
        public Guid DishId { get; set; }
        public string DishComment { get; set; }
        public ICollection<Guid> ExtraIngredientIds { get; set; }

        public AddOrderItemCommand(Guid orderItemId, string phoneNumber, int orderNo, Guid dishId, string dishComment, ICollection<Guid> extraIngredientIds)
        {
            OrderItemId = orderItemId;
            OrderNo = orderNo;
            DishId = dishId;
            DishComment = dishComment;
            ExtraIngredientIds = extraIngredientIds;
            PhoneNumber = phoneNumber;
        }
    }
}
