using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantNamesDto>> GetRestaurantNamesAsync();
        Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync();
        Task AddRestaurantAsync(CreateRestaurantCommand newRestaurant);
        Task UpdateRestaurantAsync(UpdateRestaurantCommand restaurant);
        Task<RestaurantDto> GetRestaurantAsync(Guid id);
        Task DeleteRestaurantAsync(Guid id);
    }
}
