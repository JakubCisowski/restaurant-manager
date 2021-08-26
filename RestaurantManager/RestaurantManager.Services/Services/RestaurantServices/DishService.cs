using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.Commands.RestaurantCommands.Dishes;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.DTOs.Ingredients;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices
{
    public class DishService : IDishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly ICacheService _cacheService;
        private readonly ICacheKeyService _cacheKeyService;

        public DishService(IUnitOfWork unitOfWork, ICacheService cacheService, ICacheKeyService cacheKeyService)
        {
            _unitOfWork = unitOfWork;
            _dishRepository = _unitOfWork.GetRepository<Dish>();
            _menuRepository = _unitOfWork.GetRepository<Menu>();
            _ingredientRepository = _unitOfWork.GetRepository<Ingredient>();
            _cacheService = cacheService;
            _cacheKeyService = cacheKeyService;
        }

        public async Task AddDishAsync(CreateDishCommand command)
        {
            var menu = await _menuRepository.GetByIdAsync(command.MenuId);

            if (menu is null)
            {
                throw new NotFoundException(command.MenuId, nameof(Menu));
            }

            var dish = new Dish(command.Id, command.Name, command.BasePrice, command.Description);
            dish.SetMenu(menu);

            await _dishRepository.AddAsync(dish);
            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }

        public async Task AddAvailableIngredient(AddIngredientCommand command)
        {
            var dishTask = _dishRepository.GetByIdAsync(command.DishId);
            var ingredientTask = _ingredientRepository.GetByIdAsync(command.IngredientId);
            await Task.WhenAll(dishTask, ingredientTask);

            var dish = await dishTask;
            var ingredient = await ingredientTask;

            if (dish == null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }
            if (ingredient == null)
            {
                throw new NotFoundException(command.IngredientId, nameof(Ingredient));
            }

            dish.Ingredients.Add(ingredient);
            ingredient.Dishes.Add(dish);

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }

        public async Task DeleteDishAsync(Guid id)
        {
            var deletionResult = _dishRepository.RemoveOne(x => x.Id == id);

            if (deletionResult == false)
            {
                throw new NotFoundException(id, nameof(Dish));
            }

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }

        public async Task<DishDto> GetDishAsync(Guid id)
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.DishKey, nameof(GetDishAsync), id);
            var result = await _cacheService.Get(cacheKey, () =>
            {
                var dish = _dishRepository
                   .FindMany(x => x.Id == id)
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
                   }).FirstOrDefaultAsync();

                return dish;
            }, 10);

            if (result is null)
            {
                throw new NotFoundException(id, nameof(Dish));
            }

            return result;
        }

        public async Task<IEnumerable<DishDto>> GetAllDishesAsync()
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.DishKey, nameof(GetDishAsync));
            var result = await _cacheService.Get(cacheKey, () =>
            {
                var allDishes = _dishRepository
                .GetAll()
                .Select(x => new DishDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    BasePrice = x.BasePrice,
                    Ingredients = x.Ingredients.Select(x => new IngredientBaseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price
                    }),
                    MenuId = x.Menu.Id
                }).ToListAsync();

                return allDishes;
            }, 10);

            return result;
        }

        public async Task UpdateDishAsync(UpdateDishCommand dish)
        {
            var requestedDish = await _dishRepository
                .FindOneAsync(x => x.Id == dish.Id);

            if (requestedDish == null)
            {
                throw new NotFoundException(dish.Id, nameof(Dish));
            }

            requestedDish.SetName(dish.Name);
            requestedDish.SetDescription(dish.Description);
            requestedDish.SetBasePrice(dish.BasePrice);

            _dishRepository.Update(requestedDish);
            await _unitOfWork.SaveChangesAsync();

            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }

        public async Task RemoveAvailableIngredient(RemoveIngredientCommand command)
        {
            var dishTask = _dishRepository.GetByIdAsync(command.DishId, x => x.Ingredients);
            var ingredientTask = _ingredientRepository.GetByIdAsync(command.IngredientId, x => x.Dishes);

            await Task.WhenAll(dishTask, ingredientTask);
            var dish = await dishTask;
            var ingredient = await ingredientTask;

            if (dish == null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }
            if (ingredient == null)
            {
                throw new NotFoundException(command.IngredientId, nameof(Ingredient));
            }

            dish.Ingredients.Remove(ingredient);
            ingredient.Dishes.Remove(dish);

            _dishRepository.Update(dish);
            _ingredientRepository.Update(ingredient);

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.DishKey);
        }
    }
}
