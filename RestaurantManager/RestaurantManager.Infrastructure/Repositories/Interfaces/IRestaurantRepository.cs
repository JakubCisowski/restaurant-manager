using RestaurantManager.Entities.Restaurants;
using System;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Restaurant GetBestRestaurant(string localization);
        void AddMenu(Guid restaurantId);
    }
}
