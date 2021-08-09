using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Dishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        public DishController()
        {

        }


        // POST api/<DishController>
        [HttpPost("/CreateDish")]
        public void CreateDish([FromBody] CreateDishCommand dish)
        {
        }


        // POST api/<DishController>
        [HttpPost("/AddIngrediens")]
        public void AddIngredients([FromBody] AddIngresientsCommand dish)
        {
        }


        // DELETE api/<DishController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
