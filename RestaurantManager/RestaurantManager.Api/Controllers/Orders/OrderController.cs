using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManager.Api.Controllers.Orders
{
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

        // POST api/<OrderController>
        [HttpPost("AcceptOrder")]
        public void AcceptOrder([FromBody] string value)
        {
        }

        [HttpPost("UpdateOrderDetails")]
        public void UpdateOrderDetails([FromBody] string payment, string shipment)
        {
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
