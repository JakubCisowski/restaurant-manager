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
        Task AddMenuAsync(Guid restaurantId);
        Task SetAvailableDish(SetAvailableDishCommand command);
    }
}
