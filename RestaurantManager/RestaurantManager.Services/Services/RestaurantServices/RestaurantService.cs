using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
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
        private readonly ICacheService _cacheService;
        private readonly ICacheKeyService _cacheKeyService;

        public RestaurantService(IUnitOfWork unitOfWork,
                                 ICacheService cacheService,
                                 ICacheKeyService cacheKeyService)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = _unitOfWork.GetRepository<Menu>();
            _restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
            _cacheService = cacheService;
            _cacheKeyService = cacheKeyService;
        }


        public async Task AddRestaurantAsync(CreateRestaurantCommand restaurant)
        {
            await _restaurantRepository.AddAsync(new Restaurant(restaurant.Id, restaurant.Name, restaurant.Phone, restaurant.Address));
            await _unitOfWork.SaveChangesAsync();

            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }

        public async Task DeleteRestaurantAsync(Guid id)
        {
            var deletionResult = _restaurantRepository.RemoveOne(x => x.Id == id);

            if (deletionResult == false)
            {
                throw new NotFoundException(id, nameof(Restaurant));
            }

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }

        public async Task<RestaurantDto> GetRestaurantAsync(Guid id)
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.RestaurantKey, nameof(GetRestaurantAsync), id);
            var result = await _cacheService.Get(cacheKey, () =>
            {
                var restaurantDto = _restaurantRepository
                .FindMany(x => x.Id == id)
                .Select(restaurant => new RestaurantDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    Phone = restaurant.Phone,
                    MenuId = restaurant.Menu.Id
                })
                .FirstOrDefaultAsync();

                return restaurantDto;

            }, 10);

            if (result == null)
            {
                throw new NotFoundException(id, nameof(Restaurant));
            }

            return result;
        }

        public async Task<IEnumerable<RestaurantNamesDto>> GetRestaurantNamesAsync()
        {
            var restaurantNames = await _restaurantRepository
                .FindMany(x => true)
                .Select(x => new RestaurantNamesDto
                {
                    Name = x.Name
                }).ToListAsync();

            return restaurantNames;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync()
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.RestaurantKey, nameof(GetRestaurantsAsync));
            var result = await _cacheService.Get(cacheKey, () =>
            {
                return _restaurantRepository
                 .GetAll()
                 .Select(x => new RestaurantDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Address = x.Address,
                     Phone = x.Phone,
                     MenuId = x.Menu.Id
                 }).ToListAsync();
            });

            return result;
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

            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }
    }
}
