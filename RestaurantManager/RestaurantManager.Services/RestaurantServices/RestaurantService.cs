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
        private readonly IGenericRepository<Menu> _menuRepository;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = _unitOfWork.GetRepository<Menu>();
        }

        public async Task AddMenuAsync(Guid restaurantId)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
            var menu = new Menu();
            menu.AddRestautant(restaurant);

            await _menuRepository.AddAsync(menu);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddRestaurantAsync(CreateRestaurantCommand restaurant)
        {
            await _unitOfWork.RestaurantRepository.AddAsync(new Restaurant(restaurant.Id ,restaurant.Name, restaurant.Phone, restaurant.Address));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteRestaurantAsync(Guid id)
        {
            bool deletionResult = _unitOfWork.RestaurantRepository.RemoveOne(x => x.Id == id);
            await _unitOfWork.SaveChangesAsync();
            return deletionResult;
        }

        public async Task<RestaurantsDto> GetRestaurantAsync(Guid id)
        {
            var restaurant = await _unitOfWork.RestaurantRepository
                .FindOneAsync(x => x.Id == id);

            var restaurantDto = new RestaurantsDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Phone = restaurant.Phone,
                MenuId = restaurant.Menu.Id
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

        public async Task<IEnumerable<RestaurantsDto>> GetRestaurantsAsync()
        {
            var allRestaurants = _unitOfWork.RestaurantRepository
                .GetAll()
                .Select(x => new RestaurantsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    MenuId = x.Menu.Id
                });

            return await allRestaurants.ToListAsync();
        }

        public async Task<bool> UpdateRestaurantAsync(UpdateRestaurantCommand restaurant)
        {
            var requestedRestaurant = await _unitOfWork.RestaurantRepository
                .FindOneAsync(x => x.Id == restaurant.Id);

            if (requestedRestaurant == null)
            {
                return false;
            }

            requestedRestaurant.SetName(restaurant.Name);
            requestedRestaurant.SetAddress(restaurant.Address);
            requestedRestaurant.SetPhone(restaurant.Phone);

            _unitOfWork.RestaurantRepository.Update(requestedRestaurant);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
