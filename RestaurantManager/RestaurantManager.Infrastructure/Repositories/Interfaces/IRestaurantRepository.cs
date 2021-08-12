using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Restaurant GetBestRestaurant(string localization);
    }
}
