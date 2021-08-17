using System;

namespace RestaurantManager.Services.Commands.Ingredients
{
    public class CreateIngredientCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public CreateIngredientCommand(Guid ingredientId, string name, decimal price)
        {
            Id = ingredientId;
            Name = name;
            Price = price;
        }
    }
}
