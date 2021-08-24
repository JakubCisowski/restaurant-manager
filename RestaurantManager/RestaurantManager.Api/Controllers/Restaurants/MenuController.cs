using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.DTOs;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.RestaurantServices.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Controllers.Restaurants
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ILogger _logger;

        public MenuController(IMenuService menuService,
                              ILogger logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        [HttpPost("CreateMenu")]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateMenuCommand newMenu)
        {
            try
            {
                await _menuService.AddMenuAsync(newMenu.RestaurantId);
                return Ok();
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetDishes/{menuId}")]
        public async Task<ActionResult<DishesListResponse>> GetMenuDishes(Guid menuId)
        {
            try
            {
                return await _menuService.GetDishesAsync(menuId);
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
