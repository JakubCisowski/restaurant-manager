using RestaurantManager.Entities;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
