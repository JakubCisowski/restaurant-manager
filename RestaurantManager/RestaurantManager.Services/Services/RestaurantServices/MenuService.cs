﻿using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.DTOs;
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
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly ICacheService _cacheService;
        private readonly ICacheKeyService _cacheKeyService;

        public MenuService(IUnitOfWork unitOfWork,
                           ICacheService cacheService, 
                           ICacheKeyService cacheKeyService)
        {
            _unitOfWork = unitOfWork;
            _restaurantRepository = unitOfWork.GetRepository<Restaurant>();
            _menuRepository = unitOfWork.GetRepository<Menu>();
            _dishRepository = unitOfWork.GetRepository<Dish>();
            _cacheService = cacheService;
            _cacheKeyService = cacheKeyService;
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

        public async Task<DishesListResponse> GetDishesAsync(Guid menuId)
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.DishKey, nameof(GetDishesAsync), menuId);
            var result = await _cacheService.Get(cacheKey, async () =>
            {
                if (!_menuRepository.GetAll().Any(x => x.Id == menuId))
                {
                    throw new NotFoundException(menuId, nameof(Menu));
                }

                var dishes = _dishRepository
                    .FindMany(x => x.MenuId == menuId);

                var dishesDto = dishes.Select(dish => new DishDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    BasePrice = dish.BasePrice,
                    MenuId = dish.MenuId,
                    Ingredients = dish.Ingredients
                        .Select(x => new DTOs.Ingredients.IngredientBaseDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Price = x.Price
                        })
                });

                return dishesDto;
            }, 10);

            return new DishesListResponse(result.ToList());
        }
    }
}
