using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Context;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private RestaurantDbContext _restaurantDbContext;
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(RestaurantDbContext restaurantDbContext,
                                    IRestaurantService restaurantService)
        {
            _restaurantDbContext = restaurantDbContext;
            _restaurantService = restaurantService;
        }

        // GET: api/<RestaurantController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        public IEnumerable<RestaurantNamesDto> Get(int id)
        {
            var restaurants = _restaurantService.GetRestaurantNames();

            return restaurants;
        }

        // POST api/<RestaurantController>
        [HttpPost]
        public IActionResult Post([FromQuery] string restaurantName)
        {
            var test = new Restaurant(restaurantName);
            _restaurantDbContext.Restaurants.Add(test);
            _restaurantDbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
