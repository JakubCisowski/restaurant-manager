using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;

namespace RestaurantManager.Infrastructure.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly DbSet<Menu> _menuSet;

        public RestaurantRepository(Context.RestaurantDbContext dbContext) : base(dbContext)
        {
            _menuSet = dbContext.Set<Menu>();
        }

        public Restaurant GetBestRestaurant(string localization)
        {
            throw new NotImplementedException();
        }
    }
}
