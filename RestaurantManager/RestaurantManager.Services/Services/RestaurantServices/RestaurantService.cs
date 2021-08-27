using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.RestaurantCommands.Restaurants;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.DTOs.Restaurant;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Queries.Restaurants;
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


        public async Task AddRestaurantAsync(CreateRestaurantCommand command)
        {
            await _restaurantRepository.AddAsync(new Restaurant(command.Id, command.Name, command.Phone, command.Address));
            await _unitOfWork.SaveChangesAsync();

            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }

        public async Task DeleteRestaurantAsync(DeleteRestaurantCommand command)
        {
            var deletionResult = _restaurantRepository.RemoveOne(x => x.Id == command.Id);

            if (deletionResult == false)
            {
                throw new NotFoundException(command.Id, nameof(Restaurant));
            }

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }

        public async Task<RestaurantDto> GetRestaurantAsync(GetRestaurantQuery query)
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.RestaurantKey, nameof(GetRestaurantAsync), query.Id);
            var result = await _cacheService.Get(cacheKey, () =>
            {
                var restaurantDto = _restaurantRepository
                .FindMany(x => x.Id == query.Id)
                .Select(restaurant => new RestaurantDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Address = new RestaurantAddressDto(restaurant.Address),
                    Phone = restaurant.Phone,
                    MenuId = restaurant.Menu.Id
                })
                .FirstOrDefaultAsync();

                return restaurantDto;

            }, 10);

            if (result == null)
            {
                throw new NotFoundException(query.Id, nameof(Restaurant));
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
                     Address = new RestaurantAddressDto(x.Address),
                     Phone = x.Phone,
                     MenuId = x.Menu.Id
                 }).ToListAsync();
            });

            return result;
        }

        public async Task UpdateRestaurantAsync(UpdateRestaurantCommand command)
        {
            var requestedRestaurant = await _restaurantRepository
                .FindOneAsync(x => x.Id == command.Id);

            if (requestedRestaurant == null)
            {
                throw new NotFoundException(command.Id, nameof(Restaurant));
            }

            requestedRestaurant.SetName(command.Name);
            requestedRestaurant.SetAddress(command.Address);
            requestedRestaurant.SetPhone(command.Phone);

            _restaurantRepository.Update(requestedRestaurant);
            await _unitOfWork.SaveChangesAsync();

            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }
    }
}
