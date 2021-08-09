using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddRestaurant(CreateRestaurantCommand restaurant)
        {
            _unitOfWork.RestaurantRepository.Add(new Restaurant(restaurant.Name, restaurant.Phone, restaurant.Address));
            _unitOfWork.SaveChanges();
        }

        public bool DeleteRestaurant(Guid id)
        {
            bool deletionResult =  _unitOfWork.RestaurantRepository.RemoveOne(x => x.Id == id);
            _unitOfWork.SaveChanges();
            return deletionResult;
        }

        public RestaurantsDto GetRestaurant(Guid id)
        {
            var restaurant = _unitOfWork.RestaurantRepository
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
            var restaurantNames = _unitOfWork.RestaurantRepository
                .FindMany(x => true)
                .Select(x => new RestaurantNamesDto
                {
                    Name = x.Name
                });

            return restaurantNames;
        }

        public async Task<IEnumerable<RestaurantsDto>> GetRestaurants()
        {
            var allRestaurants = _unitOfWork.RestaurantRepository
                .GetAll()
                .Select(x => new RestaurantsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone
                });

            return await allRestaurants.ToListAsync();
        }

        public bool UpdateRestaurant(UpdateRestaurantCommand restaurant)
        {
            var requestedRestaurant = _unitOfWork.RestaurantRepository
                .FindOne(x => x.Id == restaurant.Id);

            if (requestedRestaurant == null)
            {
                return false;
            }

            //SetName, SetAddress, 
            requestedRestaurant.Name = restaurant.Name;
            requestedRestaurant.Address = restaurant.Address;
            requestedRestaurant.Phone = restaurant.Phone;

            _unitOfWork.RestaurantRepository.Update(requestedRestaurant);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
