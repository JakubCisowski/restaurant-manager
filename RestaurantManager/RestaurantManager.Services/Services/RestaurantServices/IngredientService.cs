using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Core.Cache;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.Commands.RestaurantCommands.Ingredients;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.DTOs.Dishes;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Queries.RestaurantQueries.Ingredients;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly ICacheService _cacheService;
        private readonly ICacheKeyService _cacheKeyService;

        public IngredientService(IUnitOfWork unitOfWork, ICacheService cacheService, ICacheKeyService cacheKeyService)
        {
            _unitOfWork = unitOfWork;
            _dishRepository = _unitOfWork.GetRepository<Dish>();
            _ingredientRepository = _unitOfWork.GetRepository<Ingredient>();
            _cacheService = cacheService;
            _cacheKeyService = cacheKeyService;
        }

        public async Task AddIngredientAsync(CreateIngredientCommand command)
        {
            await _ingredientRepository.AddAsync(new Ingredient(command.Id, command.Name, command.Price));
            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.IngredientKey);
        }

        public async Task DeleteIngredientAsync(DeleteIngredientCommand command)
        {
            var deletionResult = _ingredientRepository.RemoveOne(x => x.Id == command.Id);

            if (deletionResult == false)
            {
                throw new NotFoundException(command.Id, nameof(Ingredient));
            }

            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveByPrefix(CachePrefixes.IngredientKey);
        }

        public async Task<IngredientDto> GetIngredientAsync(GetIngredientQuery query)
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.IngredientKey, nameof(GetIngredientAsync), query.Id);
            var result = await _cacheService.Get(cacheKey, async () =>
            {
                var ingredientDto = await _ingredientRepository
                .FindMany(x => x.Id == query.Id)
                .Select(ingredient => new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Price = ingredient.Price,
                    Dishes = ingredient.Dishes.Select(x => new DishBaseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        BasePrice = x.BasePrice,
                        Description = x.Description,
                        MenuId = x.MenuId
                    })

                })
                .FirstOrDefaultAsync();

                return ingredientDto;
            }, 10);

            return result;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var cacheKey = _cacheKeyService.GetCacheKey(CachePrefixes.IngredientKey, nameof(GetAllIngredientsAsync));
            var result = await _cacheService.Get(cacheKey, async ()  =>
            {
                var allIngredients = _ingredientRepository
                .GetAll()
                .Select(x => new IngredientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Dishes = x.Dishes.Select(x => new DishBaseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        BasePrice = x.BasePrice,
                        Description = x.Description,
                        MenuId = x.MenuId
                    })

                });

                return allIngredients;
            }, 10);

            return result;
        }

        public async Task UpdateIngredientAsync(UpdateIngredientCommand command)
        {
            var requestedIngredient = await _ingredientRepository
                .FindOneAsync(x => x.Id == command.Id);

            if (requestedIngredient == null)
            {
                throw new NotFoundException(command.Id, nameof(Ingredient));
            }

            requestedIngredient.SetName(command.Name);
            requestedIngredient.SetPrice(command.Price);

            _ingredientRepository.Update(requestedIngredient);
            await _unitOfWork.SaveChangesAsync();

            _cacheService.RemoveByPrefix(CachePrefixes.IngredientKey);
        }
    }
}
