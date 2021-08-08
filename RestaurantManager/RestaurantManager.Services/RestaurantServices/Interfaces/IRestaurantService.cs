using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantNamesDto> GetRestaurantNames();
        IEnumerable<RestaurantsDto> GetRestaurants();
        RestaurantsDto GetRestaurant(Guid id);
        bool DeleteRestaurant(Guid id);
        bool UpdateRestaurant(Guid id, Restaurant updatedRestaurant);
        void AddRestaurant(Restaurant newRestaurant);
    }
}
