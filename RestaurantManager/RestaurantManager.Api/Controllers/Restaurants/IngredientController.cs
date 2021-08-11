using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Ingredients;
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
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("AllIngredients")]
        public async Task<IEnumerable<IngredientDto>> GetAllAsync()
        {
            var result = await _ingredientService.GetAllIngredientsAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IngredientDto> GetByIdAsync(Guid id)
        {
            return await _ingredientService.GetIngredientAsync(id);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] IngredientInput input)
        {

            var ingredientId = Guid.NewGuid();
            await _ingredientService.AddIngredientAsync(
                new CreateIngredientCommand(ingredientId, input.Name, input.Price));

            return Ok(ingredientId); // todo: Handle errors
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateIngredientCommand updatedIngredient)
        {
            bool updateCompleted = await _ingredientService.UpdateIngredientAsync(updatedIngredient);
            return updateCompleted ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var deletionCompleted = await _ingredientService.DeleteIngredientAsync(id);
            return deletionCompleted ? Ok() : NotFound();
        }
    }
}
