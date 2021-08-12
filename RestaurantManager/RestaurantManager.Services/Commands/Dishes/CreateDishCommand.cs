using System;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class CreateDishCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public Guid MenuId { get; set; }

        public CreateDishCommand(Guid dishId, string name, decimal basePrice, string description, Guid menuId)
        {
            Id = dishId;
            Name = name;
            BasePrice = basePrice;
            Description = description;
            MenuId = menuId;
        }
    }
}
