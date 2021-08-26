using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.Commands.RestaurantCommands.Dishes;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Queries.RestaurantQueries.Dishes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllDishesAsync();
        Task AddDishAsync(CreateDishCommand command);
        Task UpdateDishAsync(UpdateDishCommand command);
        Task<DishDto> GetDishAsync(GetDishQuery query);
        Task DeleteDishAsync(DeleteDishCommand command);
        Task AddAvailableIngredient(AddIngredientCommand command);
        Task RemoveAvailableIngredient(RemoveIngredientCommand command);
    }
}
