using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManager.Services.RestaurantServices
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantService(IRestaurantRepository restaurantRepository,
                                 IUnitOfWork unitOfWork)
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddRestaurant(CreateRestaurantCommand restaurant)
        {
            _unitOfWork.RestaurantRepository.Add(new Restaurant(restaurant.Name, restaurant.Phone, restaurant.Address));

            _unitOfWork.SaveChanges();
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

        public bool UpdateRestaurant(UpdateRestaurantCommand restaurant)
        {
            var requestedRestaurant = _restaurantRepository
                .FindOne(x => x.Id == restaurant.Id);

            if (requestedRestaurant == null)
            {
                return false;
            }

            //SetName, SetAddress, 
            requestedRestaurant.Name = restaurant.Name;
            requestedRestaurant.Address = restaurant.Address;
            requestedRestaurant.Phone = restaurant.Phone;

            _restaurantRepository.Update(requestedRestaurant);
            return true;
        }
    }
}
