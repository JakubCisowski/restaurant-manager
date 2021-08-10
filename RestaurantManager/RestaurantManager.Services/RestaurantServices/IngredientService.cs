using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<bool> DeleteIngredientAsync(Guid id)
        {
            bool deletionResult = _ingredientRepository.RemoveOne(x => x.Id == id);
            await _unitOfWork.SaveChangesAsync();
            return deletionResult;
        }

        public async Task<IngredientsDto> GetIngredientAsync(Guid id)
        {
            var ingredient = await _ingredientRepository
                .FindOneAsync(x => x.Id == id);

            var ingredientDto = new IngredientsDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Price = ingredient.Price
            };

            return ingredientDto;
        }

        public async Task<IEnumerable<IngredientsDto>> GetIngredientsAsync()
        {
            var allIngredients = _ingredientRepository
                .GetAll()
                .Select(x => new IngredientsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                });

            return await allIngredients.ToListAsync();
        }

        public async Task<bool> UpdateIngredientAsync(UpdateIngredientCommand ingredient)
        {
            var requestedIngredient = await _ingredientRepository
                .FindOneAsync(x => x.Id == ingredient.Id);

            if (requestedIngredient == null)
            {
                return false;
            }

            requestedIngredient.SetName(ingredient.Name);
            requestedIngredient.SetPrice(ingredient.Price);

            _ingredientRepository.Update(requestedIngredient);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
