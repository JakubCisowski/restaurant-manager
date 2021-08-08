using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void AddRestaurant(Restaurant newRestaurant)
        {
            _restaurantRepository.Add(newRestaurant);
        }

        public bool DeleteRestaurant(Guid id)
        {
            return _restaurantRepository.RemoveOne(x => x.Id == id);
        }

        public RestaurantsDto GetRestaurant(Guid id)
        {
            var restaurant = _restaurantRepository
                .FindOne(x => x.Id == id);
            
            var restaurantDto = new RestaurantsDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    Phone = restaurant.Phone
                };

            return restaurantDto;
        }

        public IEnumerable<RestaurantNamesDto> GetRestaurantNames()
        {
            var restaurantNames = _restaurantRepository
                .FindMany(x => true)
                .Select(x => new RestaurantNamesDto
                {
                    Name = x.Name
                });

            return restaurantNames;
        }

        public IEnumerable<RestaurantsDto> GetRestaurants()
        {
            var allRestaurants = _restaurantRepository
                .GetAll()
                .Select(x => new RestaurantsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone
                });

            return allRestaurants;
        }

        public bool UpdateRestaurant(Guid id, Restaurant updatedRestaurant)
        {
            var requestedRestaurant = _restaurantRepository
                .FindOne(x => x.Id == id);

            if(requestedRestaurant == null)
            {
                return false;
            }

            requestedRestaurant.Name = updatedRestaurant.Name;
            requestedRestaurant.Address = updatedRestaurant.Address;
            requestedRestaurant.Phone = updatedRestaurant.Phone;

            _restaurantRepository.Update(requestedRestaurant);
            return true;
        }
    }
}
