using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Commands.RestaurantCommands.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Queries.RestaurantQueries.Menu;

namespace RestaurantManager.Services.Services.RestaurantServices.Interfaces
{
    public interface IMenuService
    {
        Task AddMenuAsync(Guid newId, CreateMenuCommand command);
        Task<DishesListResponse> GetDishesAsync(GetMenuDishesQuery query);
        Task SetAvailableDish(SetAvailableDishCommand command);
    }
}
