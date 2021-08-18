using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Orders;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.Orders;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Controllers.Orders
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public OrderController(IOrderService orderService, ILogger logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet("AllOrders")]
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return result;
        }

    [HttpGet("{id}")]
        public string GetOrder(int id)
        {
            return "value";
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _orderService.CreateOrderDraft(command);
            return result;
        }


        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemInput input)
        {
            var orderItemId = Guid.NewGuid();

            try
            {
                await _orderService.AddOrderItemAsync(
                new AddOrderItemCommand(orderItemId, input.OrderId, input.DishId, input.DishComment, input.ExtraIngredientIds));
            }
            catch (NotFoundException e)
            {
                _logger.Error(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                Problem(e.Message, "", (int)HttpStatusCode.InternalServerError);
            }

            return Ok(orderItemId);
        }

        [HttpPost("RemoveOrderItem")]
        public void RemoveOrderItem([FromBody] RemoveOrderItemCommand command)
        {
        }


        [HttpPost(nameof(SetPaymentType))]
        public void SetPaymentType([FromBody] string address)
        {

        }

        [HttpPost(nameof(SetOrderAdress))]
        public void SetOrderAdress([FromBody] string address)
        {

        }

        [HttpPost("AcceptOrder")]
        public void AcceptOrder([FromBody] string status)
        {

        }

        [HttpPost("UpdateOrderDetails")]
        public void UpdateOrderDetails([FromBody] string payment, string shipment)
        {
        }

        [HttpPost(nameof(AcceptPayment))]
        public void AcceptPayment([FromBody] string value)
        {

        }

    }
}
