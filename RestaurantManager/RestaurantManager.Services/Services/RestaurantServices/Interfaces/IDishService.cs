using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllDishesAsync();
        Task AddDishAsync(CreateDishCommand newDish);
        Task UpdateDishAsync(UpdateDishCommand dish);
        Task<DishDto> GetDishAsync(Guid id);
        Task DeleteDishAsync(Guid id);
        Task AddAvailableIngredient(AddIngredientCommand command);
    }
}
