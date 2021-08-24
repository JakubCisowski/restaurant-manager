using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.RestaurantServices
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Restaurant> _restaurantRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly ICacheService _cacheService;

        public MenuService(IUnitOfWork unitOfWork,
                           ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _restaurantRepository = unitOfWork.GetRepository<Restaurant>();
            _menuRepository = unitOfWork.GetRepository<Menu>();
            _cacheService = cacheService;
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
            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }
    }
}
