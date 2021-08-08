using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Context;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IEnumerable<RestaurantsDto> GetAll()
        {
            return _restaurantService.GetRestaurants();
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
        public void Create([FromBody] Restaurant newRestaurant)
        {
            _restaurantService.AddRestaurant(newRestaurant);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Restaurant updatedRestaurant)
        {
            bool updateCompleted = _restaurantService.UpdateRestaurant(id, updatedRestaurant);
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
