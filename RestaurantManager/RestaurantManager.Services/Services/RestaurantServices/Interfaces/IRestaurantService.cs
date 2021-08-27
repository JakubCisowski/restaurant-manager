using RestaurantManager.Services.Commands.RestaurantCommands.Restaurants;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Queries.Restaurants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantNamesDto>> GetRestaurantNamesAsync();
        Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync();
        Task AddRestaurantAsync(CreateRestaurantCommand command);
        Task UpdateRestaurantAsync(UpdateRestaurantCommand command);
        Task<RestaurantDto> GetRestaurantAsync(GetRestaurantQuery query);
        Task DeleteRestaurantAsync(DeleteRestaurantCommand command);
    }
}
