using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
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

        //[HttpGet]
        //public IEnumerable<RestaurantsDto> GetAll()
        //{
        //    return _restaurantService.GetRestaurants();
        //}

        [HttpGet]
        public async Task<IEnumerable<RestaurantsDto>> GetAllAsync()
        {
            var result = await _restaurantService.GetRestaurants();

            return result;
        }

        [HttpGet("{id}")]
        public RestaurantsDto GetById(Guid id)
        {
            return _restaurantService.GetRestaurant(id);
        }

        [HttpGet("names")]
        public IEnumerable<RestaurantNamesDto> GetNames()
        {
            return _restaurantService.GetRestaurantNames();
        }

        [HttpPost]
        public void Create([FromBody] CreateRestaurantCommand newRestaurant)
        {
            _restaurantService.AddRestaurant(newRestaurant);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateRestaurantCommand updatedRestaurant)
        {
            bool updateCompleted = _restaurantService.UpdateRestaurant(updatedRestaurant);
            return updateCompleted ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            bool deletionCompleted = _restaurantService.DeleteRestaurant(id);
            return deletionCompleted ? Ok() : NotFound();
        }
    }
}
