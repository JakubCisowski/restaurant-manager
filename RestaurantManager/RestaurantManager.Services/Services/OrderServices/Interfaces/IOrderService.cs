using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderDraft(CreateOrderCommand command);
        Task AddOrderItemAsync(AddOrderItemCommand newOrderItem);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task DeleteOrderItemAsync(Guid id);
        Task<OrderDetailsDto> GetOrderDetailsAsync(string phone, int orderNo);
        Task AddOrderAddress(AddAddressCommand command);
        Task SetPaymentMethod(SetPaymentMethodCommand command);
        Task ConfirmOrder(AcceptOrderCommand command);
        Task<DinnerBillDto> GetDinnerBillAsync(int orderNo, string phone);
        Task AcceptPaymentAsync(int orderNo, string phone);
        Task<OrdersListResponse> CustomerOrders(string phone);
        Task<OrdersListResponse> RestaurantOrders(Guid restaurantId);
    }
}
