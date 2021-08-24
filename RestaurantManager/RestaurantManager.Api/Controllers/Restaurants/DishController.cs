using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Dishes;
using RestaurantManager.Services.Commands.RestaurantCommands.Dishes;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : BaseApiController
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService, ILogger logger)
            : base(logger)
        {
            _dishService = dishService;
        }

        [HttpGet("AllDishes")]
        public async Task<IEnumerable<DishDto>> GetAllAsync()
        {
            var result = await _dishService.GetAllDishesAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishDto>> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dishService.GetDishAsync(id);
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] DishInput input)
        {
            var dishId = Guid.NewGuid();

            try
            {
                await _dishService.AddDishAsync(
                new CreateDishCommand(dishId, input.Name, input.BasePrice, input.Description, input.MenuId));
            }
            catch (Exception e) { return ReturnException(e); }

            return Ok(dishId);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDishCommand updatedDish)
        {
            try
            {
                await _dishService.UpdateDishAsync(updatedDish);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _dishService.DeleteDishAsync(id);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("AddAvailableIngredient")]
        public async Task<IActionResult> AddAvailableIngredient([FromBody] AddIngredientCommand command)
        {
            try
            {
                await _dishService.AddAvailableIngredient(command);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpDelete("RemoveAvailableIngredient")]
        public async Task<IActionResult> RemoveAvailableIngredient([FromBody] RemoveIngredientCommand command)
        {
            try
            {
                await _dishService.RemoveAvailableIngredient(command);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }
    }
}
