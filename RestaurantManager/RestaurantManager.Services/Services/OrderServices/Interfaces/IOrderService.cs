using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.Orders;
using RestaurantManager.Services.Queries.OrdersQueries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderDraft(CreateOrderCommand command);
        Task AddOrderItemAsync(AddOrderItemCommand command);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task DeleteOrderItemAsync(DeleteOrderItemCommand command);
        Task AddOrderAddress(AddAddressCommand command);
        Task SetPaymentMethod(SetPaymentMethodCommand command);
        Task ConfirmOrder(AcceptOrderCommand command);
        Task AcceptPaymentAsync(AcceptPaymentCommand command);
        Task<OrderDetailsDto> GetOrderDetailsAsync(GetOrderDetailsQuery query);
        Task<DinnerBillDto> GetDinnerBillAsync(GetDinnerBillQuery query);
        Task<OrdersListResponse> CustomerOrders(GetCustomerOrdersQuery query);
        Task<OrdersListResponse> RestaurantOrders(GetRestaurantOrdersQuery query);
    }
}
