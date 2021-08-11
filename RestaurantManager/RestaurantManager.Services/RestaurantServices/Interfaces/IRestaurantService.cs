using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantNamesDto> GetRestaurantNames();
        Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync();
        Task AddRestaurantAsync(CreateRestaurantCommand newRestaurant);
        Task AddMenuAsync(Guid restaurantId);
        Task<bool> UpdateRestaurantAsync(UpdateRestaurantCommand restaurant);
        Task<RestaurantDto> GetRestaurantAsync(Guid id);
        Task<bool> DeleteRestaurantAsync(Guid id);
    }
}
