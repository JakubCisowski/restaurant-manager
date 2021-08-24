using System;

namespace RestaurantManager.Services.Services.OrderServices
{
    internal class OrderItemsDto
    {
        public Guid Id { get; set; }
        public object Name { get; set; }
        public object Price { get; set; }
    }
}