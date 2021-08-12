using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task AddIngredientAsync(CreateIngredientCommand newIngredient);
        Task UpdateIngredientAsync(UpdateIngredientCommand ingredient);
        Task<IngredientDto> GetIngredientAsync(Guid id);
        Task DeleteIngredientAsync(Guid id);
    }
}
