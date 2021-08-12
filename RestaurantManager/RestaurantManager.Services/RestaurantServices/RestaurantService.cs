using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
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
        private readonly IGenericRepository<Restaurant> _restaurantRepository;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = _unitOfWork.GetRepository<Menu>();
            _restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
        }

        public async Task AddMenuAsync(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(restaurantId, nameof(Restaurant));
            }

            var menu = new Menu();
            await _menuRepository.AddAsync(menu);

            restaurant.AddMenu(menu);
            _restaurantRepository.Update(restaurant);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddRestaurantAsync(CreateRestaurantCommand restaurant)
        {
            await _restaurantRepository.AddAsync(new Restaurant(restaurant.Id, restaurant.Name, restaurant.Phone, restaurant.Address));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRestaurantAsync(Guid id)
        {
            var deletionResult = _restaurantRepository.RemoveOne(x => x.Id == id);

            if (deletionResult == false)
            {
                throw new NotFoundException(id, nameof(Restaurant));
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RestaurantDto> GetAllRestaurantAsync(Guid id)
        {
            var restaurant = await _restaurantRepository
                .FindOneAsync(x => x.Id == id);

            if (restaurant == null)
            {
                throw new NotFoundException(id, nameof(Restaurant));
            }

            var restaurantDto = new RestaurantDto
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
            var restaurantNames = _restaurantRepository
                .FindMany(x => true)
                .Select(x => new RestaurantNamesDto
                {
                    Name = x.Name
                });

            return restaurantNames;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync()
        {
            var allRestaurants = _restaurantRepository
                .GetAll()
                .Select(x => new RestaurantDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    MenuId = x.Menu.Id
                });

            return await allRestaurants.ToListAsync();
        }

        public async Task UpdateRestaurantAsync(UpdateRestaurantCommand restaurant)
        {
            var requestedRestaurant = await _restaurantRepository
                .FindOneAsync(x => x.Id == restaurant.Id);

            if (requestedRestaurant == null)
            {
                throw new NotFoundException(restaurant.Id, nameof(Restaurant));
            }

            requestedRestaurant.SetName(restaurant.Name);
            requestedRestaurant.SetAddress(restaurant.Address);
            requestedRestaurant.SetPhone(restaurant.Phone);

            _restaurantRepository.Update(requestedRestaurant);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
