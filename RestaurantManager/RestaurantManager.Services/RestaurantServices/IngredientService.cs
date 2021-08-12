using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.DTOs.Dishes;
using RestaurantManager.Services.Exceptions;
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

        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dishRepository = _unitOfWork.GetRepository<Dish>();
            _ingredientRepository = _unitOfWork.GetRepository<Ingredient>();
        }

        public async Task AddIngredientAsync(CreateIngredientCommand newIngredient)
        {
            await _ingredientRepository.AddAsync(new Ingredient(newIngredient.Id, newIngredient.Name, newIngredient.Price));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(Guid id)
        {
            var deletionResult = _ingredientRepository.RemoveOne(x => x.Id == id);

            if (deletionResult == false)
            {
                throw new NotFoundException(id, nameof(Ingredient));
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IngredientDto> GetIngredientAsync(Guid id)
        {
            var ingredientDto = await _ingredientRepository
                .FindMany(x => x.Id == id)
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

            //var ingredientDto = new IngredientDto
            //{
            //    Id = ingredient.Id,
            //    Name = ingredient.Name,
            //    Price = ingredient.Price,
            //    Dishes = ingredient.Dishes.Select(x => new DishBaseDto
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        BasePrice = x.BasePrice,
            //        Description = x.Description,
            //        MenuId = x.MenuId
            //    })

            //};

            return ingredientDto;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
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

            return await allIngredients.ToListAsync();
        }

        public async Task UpdateIngredientAsync(UpdateIngredientCommand ingredient)
        {
            var requestedIngredient = await _ingredientRepository
                .FindOneAsync(x => x.Id == ingredient.Id);

            if (requestedIngredient == null)
            {
                throw new NotFoundException(ingredient.Id, nameof(Ingredient));
            }

            requestedIngredient.SetName(ingredient.Name);
            requestedIngredient.SetPrice(ingredient.Price);

            _ingredientRepository.Update(requestedIngredient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
