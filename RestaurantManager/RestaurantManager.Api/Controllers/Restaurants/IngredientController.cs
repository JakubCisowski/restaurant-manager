using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Ingredients;
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
        public async Task<ActionResult<IngredientDto>> GetByIdAsync(Guid id)
        {
            try
            {
                return await _ingredientService.GetIngredientAsync(id);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] IngredientInput input)
        {

            var ingredientId = Guid.NewGuid();

            try
            {
                await _ingredientService.AddIngredientAsync(
                new CreateIngredientCommand(ingredientId, input.Name, input.Price));
            }
            catch (Exception)
            {
                Problem("Error", "", (int)HttpStatusCode.InternalServerError);
            }

            return Ok(ingredientId);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateIngredientCommand updatedIngredient)
        {
            try
            {
                await _ingredientService.UpdateIngredientAsync(updatedIngredient);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _ingredientService.DeleteIngredientAsync(id);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
