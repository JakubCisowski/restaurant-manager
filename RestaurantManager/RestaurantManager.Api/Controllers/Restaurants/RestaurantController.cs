using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Commands.Restaurants;
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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger _logger;

        public RestaurantController(IRestaurantService restaurantService,
                                    ILogger logger)
        {
            _restaurantService = restaurantService;
            _logger = logger;
        }

        [HttpGet("AllRestaurants")]
        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var result = await _restaurantService.GetRestaurantsAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetByIdAsync(Guid id)
        {
            try
            {
                return await _restaurantService.GetRestaurantAsync(id);
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
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
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }

            return Ok(restaurantId);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRestaurantCommand updatedRestaurant)
        {
            try
            {
                await _restaurantService.UpdateRestaurantAsync(updatedRestaurant);
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
                await _restaurantService.DeleteRestaurantAsync(id);
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

      
    }
}
