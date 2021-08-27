using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Commands.RestaurantCommands.Menu;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Queries.RestaurantQueries.Menu;
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
            _cacheService = cacheService;
            _dishRepository = unitOfWork.GetRepository<Dish>();
            _cacheKeyService = cacheKeyService;
        }

        public async Task AddMenuAsync(Guid newId, CreateMenuCommand command)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(command.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(command.RestaurantId, nameof(Restaurant));
            }


            var menu = new Menu(newId);
            await _menuRepository.AddAsync(menu);

            restaurant.AddMenu(menu);
            _restaurantRepository.Update(restaurant);

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.RestaurantKey);
        }

        public async Task<DishesListResponse> GetDishesAsync(GetMenuDishesQuery query)
        {
            if (!_menuRepository.FindMany(x => x.Id == query.MenuId).Any())
            {
                throw new NotFoundException(query.MenuId, nameof(Menu));
            }

            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.DishKey, nameof(GetDishesAsync), query.MenuId, query.DisplayNonAvailableDishes);
            var result = await _cacheService.Get(cacheKey, () =>
            {
                var dishesDto = _dishRepository
                    .FindMany(x => x.MenuId == query.MenuId && ( x.IsAvailable == !query.DisplayNonAvailableDishes || x.IsAvailable))
                    .Select(dish => new DishDto
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
                    }).ToListAsync();

                return dishesDto;
            }, 10);

            return new DishesListResponse(result.ToList());
        }

        public async Task SetAvailableDish(SetAvailableDishCommand command)
        {
            var dish = await _dishRepository
                .FindOneAsync(x => x.MenuId == command.MenuId && x.Id == command.DishId);

            if (dish is null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }

            dish.SetAvailability(command.IsAvailable);
            _dishRepository.Update(dish);

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }
    }
}
