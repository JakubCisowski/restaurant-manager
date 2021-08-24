using RestaurantManager.Services.DTOs.Ingredients;
using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.DTOs
{
    public class DishesListResponse
    {
        public DishesListResponse(List<DishDto> dishDtos)
        {
            Dishes = dishDtos;
        }

        public IEnumerable<DishDto> Dishes { get; set; }
    }

    public class DishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public Guid? MenuId { get; set; }
        public IEnumerable<IngredientBaseDto> Ingredients { get; set; }

    }
}
