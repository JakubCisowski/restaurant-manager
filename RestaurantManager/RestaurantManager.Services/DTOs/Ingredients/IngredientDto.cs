using RestaurantManager.Services.DTOs.Dishes;
using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.DTOs
{
    public class IngredientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<DishBaseDto> Dishes { get; set; }
    }


}
