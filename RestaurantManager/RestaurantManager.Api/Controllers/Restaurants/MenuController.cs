using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Commands.RestaurantCommands.Menu;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.RestaurantServices.Interfaces;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using RestaurantManager.Services.Queries.RestaurantQueries.Menu;

namespace RestaurantManager.Api.Controllers.Restaurants
{
    public class MenuController : BaseApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService,
                              ILogger logger) : base(logger)
        {
            _menuService = menuService;

        }

        [HttpPost("CreateMenu")]
        public async Task<IActionResult> CreateMenuAsync([FromQuery] Guid restaurantId)
        {
            try
            {
                var menuId = Guid.NewGuid();
                await _menuService.AddMenuAsync(new CreateMenuCommand(menuId, restaurantId));
                return Ok(menuId);
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpGet("GetDishes")]
        public async Task<ActionResult<DishesListResponse>> GetMenuDishes(Guid menuId, bool displayNonAvailableDishes = false)
        {
            try
            {
                return await _menuService.GetDishesAsync(new GetMenuDishesQuery(menuId, displayNonAvailableDishes));
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }


        [HttpPost("SetAvailableDish")]
        public async Task<IActionResult> SetAvailableDishAsync([FromBody] SetAvailableDishCommand command)
        {
            try
            {
                await _menuService.SetAvailableDish(command);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

    }
}
