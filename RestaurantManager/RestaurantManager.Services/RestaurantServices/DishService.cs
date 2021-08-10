using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices
{
    public class DishService : IDishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Dish> _dishRepository;

        public DishService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dishRepository = _unitOfWork.GetRepository<Dish>();
        }

        public async Task AddDishAsync(CreateDishCommand newDish)
        {
            await _dishRepository.AddAsync(new Dish(newDish.Id, newDish.Name, newDish.BasePrice, newDish.Description, newDish.MenuId));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteDishAsync(Guid id)
        {
            bool deletionResult = _dishRepository.RemoveOne(x => x.Id == id);
            await _unitOfWork.SaveChangesAsync();
            return deletionResult;
        }

        public async Task<DishesDto> GetDishAsync(Guid id)
        {
            var dish = await _dishRepository
                .FindOneAsync(x => x.Id == id);

            var dishDto = new DishesDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                BasePrice = dish.BasePrice,
                Ingredients = dish.Ingredients,
                MenuId = dish.MenuId
            };

            return dishDto;
        }

        public async Task<IEnumerable<DishesDto>> GetDishesAsync()
        {
            var allDishes = _dishRepository
                .GetAll()
                .Select(x => new DishesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    BasePrice = x.BasePrice,
                    Ingredients = x.Ingredients,
                    MenuId = x.Menu.Id
                });

            return await allDishes.ToListAsync();
        }

        public async Task<bool> UpdateDishAsync(UpdateDishCommand dish)
        {
            var requestedDish = await _dishRepository
                .FindOneAsync(x => x.Id == dish.Id);

            if (requestedDish == null)
            {
                return false;
            }

            requestedDish.SetName(dish.Name);
            requestedDish.SetDescription(dish.Description);
            requestedDish.SetBasePrice(dish.BasePrice);
            requestedDish.SetMenuId(dish.MenuId);

            _dishRepository.Update(requestedDish);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
