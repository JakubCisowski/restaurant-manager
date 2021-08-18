using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderDraft(CreateOrderCommand command);
        Task AddOrderItemAsync(AddOrderItemCommand newOrderItem);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    }
}
