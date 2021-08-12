using System;

namespace RestaurantManager.Services.Commands.Ingredients
{
    public class UpdateIngredientCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
