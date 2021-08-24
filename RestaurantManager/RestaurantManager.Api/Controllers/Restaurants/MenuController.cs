﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Menu;
using RestaurantManager.Services.Commands.RestaurantCommands.Menu;
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

        [HttpPost("SetAvailableDish")]
        public async Task<IActionResult> SetAvailableDishAsync([FromBody] SetAvailableDishCommand command)
        {
            try
            {
                await _menuService.SetAvailableDish(command);
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

    }
}
