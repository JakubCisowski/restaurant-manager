using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.Commands.RestaurantCommands.Ingredients;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Queries.RestaurantQueries.Ingredients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task AddIngredientAsync(CreateIngredientCommand command);
        Task UpdateIngredientAsync(UpdateIngredientCommand command);
        Task<IngredientDto> GetIngredientAsync(GetIngredientQuery query);
        Task DeleteIngredientAsync(DeleteIngredientCommand command);
    }
}
