using System;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class AddIngredientCommand
    {
        public Guid DishId { get; set; }
        public Guid IngredientId { get; set; }
    }
}
