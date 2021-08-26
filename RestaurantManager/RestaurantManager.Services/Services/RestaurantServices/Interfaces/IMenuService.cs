using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Commands.RestaurantCommands.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.RestaurantServices.Interfaces
{
    public interface IMenuService
    {
        Task AddMenuAsync(Guid restaurantId, Guid restaurantId1);
        Task<DishesListResponse> GetDishesAsync(Guid menuId, bool displayNonAvailableDishes);
        Task SetAvailableDish(SetAvailableDishCommand command);
    }
}
