using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Ingredients;
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
    //[Authorize] for test
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : BaseApiController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService, ILogger logger)
            : base(logger)
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
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] IngredientInput input)
        {
            try
            {
                var ingredientId = Guid.NewGuid();
                await _ingredientService.AddIngredientAsync(
                new CreateIngredientCommand(ingredientId, input.Name, input.Price));
                return Ok(ingredientId);
            }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateIngredientCommand updatedIngredient)
        {
            try
            {
                await _ingredientService.UpdateIngredientAsync(updatedIngredient);
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
                await _ingredientService.DeleteIngredientAsync(id);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }
    }
}
