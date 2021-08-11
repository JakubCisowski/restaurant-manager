using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("AllRestaurants")]
        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var result = await _restaurantService.GetRestaurantsAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<RestaurantDto> GetByIdAsync(Guid id)
        {
            return await _restaurantService.GetRestaurantAsync(id);
        }

        [HttpGet("RestaurantNames")]
        public IEnumerable<RestaurantNamesDto> GetNames()
        {
            return _restaurantService.GetRestaurantNames();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] RestaurantInput input)
        {
            var restaurantId = Guid.NewGuid();
            try
            {
                await _restaurantService.AddRestaurantAsync(
                    new CreateRestaurantCommand(restaurantId, input.Name, input.Phone, input.Address));
            }
            catch (Exception e)
            {
                Problem("Error", "", (int)HttpStatusCode.InternalServerError);
            }

            return Ok(restaurantId); // todo: Handle errors
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRestaurantCommand updatedRestaurant)
        {
            try
            {
                bool updateCompleted = await _restaurantService.UpdateRestaurantAsync(updatedRestaurant);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var deletionCompleted = await _restaurantService.DeleteRestaurantAsync(id);
            return deletionCompleted ? Ok() : NotFound();
        }

        [HttpPost("CreateMenu")]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateMenuCommand newMenu)
        {
            await _restaurantService.AddMenuAsync(newMenu.RestaurantId);
            return Ok();
        }
    }
}
