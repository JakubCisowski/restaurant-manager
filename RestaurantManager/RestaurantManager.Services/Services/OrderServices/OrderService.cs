using RestaurantManager.Entities.Orders;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<ShippingAddress> _addressRepository;
        private readonly ICustomerService _customerService;
        private readonly IOrderNoGeneratorService _orderNoGeneratorService;

        public OrderService(IUnitOfWork unitOfWork,
                            ICustomerService customerService,
                            IOrderNoGeneratorService orderNoGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.GetRepository<Order>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _addressRepository = _unitOfWork.GetRepository<ShippingAddress>();
            _customerService = customerService;
            _orderNoGeneratorService = orderNoGeneratorService;
        }

        public async Task<int> CreateOrderDraft(CreateOrderCommand command)
        {
            var order = new Order();
            order.SetCustomer(await GetCustomer(command.Phone));
            order.SetOrderNumber(_orderNoGeneratorService.GenerateOrderNo());

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order.OrderNo;
        }

        public async Task AddOrderAddress(AddAddressCommand command)
        {
            var order = await _orderRepository.FindOneAsync(x => x.OrderNo == command.OrderNo);

            if (order is null)
            {
                //return new NotFoundException(command.OrderNo)
                // do weryfikacji czy odpytujemy po orderNo
            }

            var address = new ShippingAddress(command.Country, command.City, command.Address1, command.Address2, command.PhoneNumber, command.ZipPostalCode);
            await _addressRepository.AddAsync(address);

            order.SetAddress(address);
            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<Customer> GetCustomer(string phone)
        {
            var customer = await _customerService.GetCustomer(phone);

            if (customer is null)
            {
                customer = await _customerService.CreateCustomer(phone);
            }

            return customer;
        }

        public async Task SetPaymentMethod(SetPaymentMethodCommand command)
        {
            var order = await _orderRepository.FindOneAsync(x => x.OrderNo == command.OrderNo);
            order.SetPaymentMethod(command.PaymentType);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmOrder(AcceptOrderCommand command)
        {
            var order = await _orderRepository.FindOneAsync(x => x.OrderNo == command.OrderNo);
            order.SetAsConfirmed();

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
