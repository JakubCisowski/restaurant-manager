using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Dishes;

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
        public void AddIngredients([FromBody] AddIngredientsCommand dish)
        {
        }


        // DELETE api/<DishController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
