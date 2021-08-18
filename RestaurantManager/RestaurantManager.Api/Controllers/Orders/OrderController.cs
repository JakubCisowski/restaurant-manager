using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System.Collections.Generic;
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
