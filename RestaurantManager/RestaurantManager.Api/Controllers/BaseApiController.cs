using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.ErrorResponses;
using RestaurantManager.Services.Exceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger _logger;
        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }
        protected ActionResult ReturnException(Exception e)
        {
            _logger.Error(e.Message);
            return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
        }

        protected  ActionResult ReturnException(OrderNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(new OrderErrorResponse(e.OrderNo, e.Phone, e.Message));
        }

        protected ActionResult ReturnException(NotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }

        protected ActionResult ReturnException(IncorrectOrderStatus e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }

        protected ActionResult ReturnException(DishDoesNotContainIngredientException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
    }
}
