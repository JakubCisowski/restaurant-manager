using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Dishes;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly ILogger _logger;

        public DishController(IDishService dishService, ILogger logger)
        {
            _dishService = dishService;
            _logger = logger;
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
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
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
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }

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
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _dishService.DeleteDishAsync(id);
                return Ok();
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("AddAvailableIngredient")]
        public async Task<IActionResult> AddAvailableIngredient([FromBody] AddIngredientCommand command)
        {
            try
            {
                await _dishService.AddAvailableIngredient(command); return Ok();
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
