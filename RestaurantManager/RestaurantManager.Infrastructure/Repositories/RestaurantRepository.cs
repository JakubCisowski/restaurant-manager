using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;

namespace RestaurantManager.Infrastructure.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(Context.RestaurantDbContext dbContext) : base(dbContext)
        {
        }

        public Restaurant GetBestRestaurant(string localization)
        {
            throw new NotImplementedException();
        }
    }
}
