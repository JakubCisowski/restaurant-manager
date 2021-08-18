using System;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class RemoveOrderItemCommand
    {
        public Guid OrderItemId { get; set; }
    }
}
