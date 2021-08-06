using RestaurantManager.Entities;
using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Restaurant GetBestRestaurant(string localization);
    }
}
