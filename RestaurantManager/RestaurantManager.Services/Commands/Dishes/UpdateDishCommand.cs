using System;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class UpdateDishCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
    }
}
