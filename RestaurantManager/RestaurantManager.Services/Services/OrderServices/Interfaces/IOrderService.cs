using RestaurantManager.Services.Commands.OrdersCommands;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderDraft(CreateOrderCommand command);
        Task AddOrderAddress(AddAddressCommand command);
        Task SetPaymentMethod(SetPaymentMethodCommand command);
        Task ConfirmOrder(AcceptOrderCommand command);
    }
}
