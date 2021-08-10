using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishesDto>> GetDishesAsync();
        Task AddDishAsync(CreateDishCommand newDish);
        Task<bool> UpdateDishAsync(UpdateDishCommand dish);
        Task<DishesDto> GetDishAsync(Guid id);
        Task<bool> DeleteDishAsync(Guid id);
        Task AddIngredient(AddIngredientCommand command);
    }
}
