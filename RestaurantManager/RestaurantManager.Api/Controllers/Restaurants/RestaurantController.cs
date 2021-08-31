using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Commands.RestaurantCommands.Restaurants;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Queries.Restaurants;
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
    public class RestaurantController : BaseApiController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService,
                                    ILogger logger) : base(logger)
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
        public async Task<ActionResult<RestaurantDto>> GetByIdAsync(Guid id)
        {
            try
            {
                return await _restaurantService.GetRestaurantAsync(new GetRestaurantQuery(id));
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpGet("RestaurantNames")]
        public async Task<IEnumerable<RestaurantNamesDto>> GetNames()
        {
            return await _restaurantService.GetRestaurantNamesAsync();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] RestaurantInput input)
        {
            try
            {
                var restaurantId = Guid.NewGuid();
                await _restaurantService.AddRestaurantAsync(
                    new CreateRestaurantCommand(restaurantId, input.Name, input.Phone, input.Address, input.MaxShippingDistanceRadius));
                return Ok(restaurantId);
            }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRestaurantCommand updatedRestaurant)
        {
            try
            {
                await _restaurantService.UpdateRestaurantAsync(updatedRestaurant);
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
                await _restaurantService.DeleteRestaurantAsync(new DeleteRestaurantCommand(id));
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }
    }
}
