using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestaurantManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("AllDishes")]
        public async Task<IEnumerable<DishesDto>> GetAllAsync()
        {
            var result = await _dishService.GetDishesAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<DishesDto> GetByIdAsync(Guid id)
        {
            return await _dishService.GetDishAsync(id);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] DishInput input)
        {

            var dishId = Guid.NewGuid();
            await _dishService.AddDishAsync(
                new CreateDishCommand(dishId, input.Name, input.BasePrice, input.Description, input.MenuId));

            return Ok(dishId); // todo: Handle errors
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDishCommand updatedDish)
        {
            bool updateCompleted = await _dishService.UpdateDishAsync(updatedDish);
            return updateCompleted ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var deletionCompleted = await _dishService.DeleteDishAsync(id);
            return deletionCompleted ? Ok() : NotFound();
        }

        [HttpPost("AddAvailableIngredient")]
        public async Task<IActionResult> AddAvailableIngredient([FromBody] AddIngredientCommand command)
        {
            await _dishService.AddAvailableIngredient(command);

            return Ok(command);
        }

    }
}
