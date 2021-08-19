using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Inputs.Orders;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string GetOrder(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _orderService.CreateOrderDraft(command);
            return result;
        }


        [HttpPost("AddOrderItem")]
        public void AddOrderItem([FromBody] AddOrderItemCommand command)
        {

        }

        [HttpPost("RemoveOrderItem")]
        public void RemoveOrderItem([FromBody] RemoveOrderItemCommand command)
        {
        }


        [HttpPost("AddOrderAddress")]
        public async Task<IActionResult> AddOrderAddressAsync([FromBody] AddAddressCommand command)
        {
            try
            {
                await _orderService.AddOrderAddress(command);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("SetPaymentMethod")]
        public async Task<IActionResult> SetPaymentMethodAsync([FromBody] SetPaymentMethodCommand command)
        {
            try
            {
                await _orderService.SetPaymentMethod(command);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("AcceptOrder")]
        public async Task<IActionResult> AcceptOrderAsync([FromBody] AcceptOrderCommand command)
        {
            try
            {
                await _orderService.ConfirmOrder(command);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost(nameof(AcceptPayment))]
        public async Task<IActionResult> AcceptPayment([FromBody] AcceptPaymentInput input)
        {

            return Ok();
        }
    }
}
