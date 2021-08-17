using System;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class RemoveOrderItemCommand
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
    }

}
