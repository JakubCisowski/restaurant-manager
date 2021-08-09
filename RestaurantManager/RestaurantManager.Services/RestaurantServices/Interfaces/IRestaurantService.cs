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
        Task<IEnumerable<RestaurantsDto>> GetRestaurants();
        RestaurantsDto GetRestaurant(Guid id);
        bool DeleteRestaurant(Guid id);
        bool UpdateRestaurant(UpdateRestaurantCommand updatedRestaurant);
        void AddRestaurant(CreateRestaurantCommand newRestaurant);
        void AddMenu(Guid restaurantId);
    }
}
