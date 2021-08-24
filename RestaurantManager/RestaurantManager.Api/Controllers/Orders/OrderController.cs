using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManager.Api.ErrorResponses;
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
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(
            IOrderService orderService,
            ILogger logger) : base(logger)
        {
            _orderService = orderService;
        }

        [HttpGet("AllOrders")]
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return result;
        }

        [HttpGet("OrdersByPhone")]
        public async Task<ActionResult<OrdersListResponse>> GetOrdersByPhone([FromQuery] string phone, [FromQuery] int orderNo)
        {
            try
            {
                return await _orderService.GetOrdersAsync(phone, orderNo);
            }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpGet("DinnerBill")]
        public async Task<ActionResult<DinnerBillDto>> GetDinnerBill([FromQuery] int orderNo, [FromQuery] string phone)
        {
            try
            {
                return await _orderService.GetDinnerBillAsync(orderNo, phone);
            }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                var result = await _orderService.CreateOrderDraft(command);
                return result;
            }
            catch (Exception e)
            {
                return ReturnException(e);
            }
        }


        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemInput input)
        {
            var orderItemId = Guid.NewGuid();

            try
            {
                await _orderService.AddOrderItemAsync(
                    new AddOrderItemCommand(orderItemId, input.OrderNo, input.DishId, input.DishComment, input.ExtraIngredientIds));
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (DishDoesNotContainIngredientException e) { return ReturnException(e); }
            catch (IncorrectOrderStatus e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }

            return Ok(orderItemId);
        }



        [HttpDelete("RemoveOrderItem/{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _orderService.DeleteOrderItemAsync(id);
                return Ok();
            }
            catch (NotFoundException e) { return ReturnException(e); }
            catch (IncorrectOrderStatus e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("AddOrderAddress")]
        public async Task<IActionResult> AddOrderAddressAsync([FromBody] AddAddressCommand command)
        {
            try
            {
                await _orderService.AddOrderAddress(command);
                return Ok();
            }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (IncorrectOrderStatus e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }

        [HttpPost("SetPaymentMethod")]
        public async Task<IActionResult> SetPaymentMethodAsync([FromBody] SetPaymentMethodCommand command)
        {
            try
            {
                await _orderService.SetPaymentMethod(command);
                return Ok();
            }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (IncorrectOrderStatus e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
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
                return ReturnException(e);
            }
        }

        [HttpPost(nameof(AcceptPayment))]
        public async Task<IActionResult> AcceptPayment([FromBody] AcceptPaymentInput input)
        {
            try
            {
                await _orderService.AcceptPaymentAsync(input.OrderNo, input.Phone);
                return Ok();
            }
            catch (OrderNotFoundException e) { return ReturnException(e); }
            catch (IncorrectOrderStatus e) { return ReturnException(e); }
            catch (Exception e) { return ReturnException(e); }
        }
    }
}
