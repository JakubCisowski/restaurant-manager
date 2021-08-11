using System;

namespace RestaurantManager.Services.DTOs.Ingredients
{
    public class IngredientBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
