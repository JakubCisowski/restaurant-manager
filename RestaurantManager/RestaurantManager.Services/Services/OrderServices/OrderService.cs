using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.OrderItems;
using RestaurantManager.Services.DTOs.Orders;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<ShippingAddress> _addressRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<DishExtraIngredient> _dishExtraIngredientRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IOrderNoGeneratorService _orderNoGeneratorService;
        private readonly IOrderCalculationService _orderCalculationService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork unitOfWork,
                            IOrderRepository orderRepository,
                            ICustomerService customerService,
                            IOrderNoGeneratorService orderNoGeneratorService,
                            IOrderCalculationService orderCalculationService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _dishRepository = _unitOfWork.GetRepository<Dish>();
            _ingredientRepository = _unitOfWork.GetRepository<Ingredient>();
            _dishExtraIngredientRepository = _unitOfWork.GetRepository<DishExtraIngredient>();
            _customerRepository = _unitOfWork.GetRepository<Customer>();
            _addressRepository = _unitOfWork.GetRepository<ShippingAddress>();
            _customerService = customerService;
            _orderNoGeneratorService = orderNoGeneratorService;
            _orderCalculationService = orderCalculationService;
        }

        public async Task AddOrderItemAsync(AddOrderItemCommand command)
        {
            var order = await _orderRepository
                .FindOneOrder(command.OrderNo, command.PhoneNumber);

            var dish = await _dishRepository.GetByIdAsync(command.DishId);

            var ingredients = _ingredientRepository
                .FindMany(x => command.ExtraIngredientIds.Contains(x.Id)
                          && x.Dishes.Any(x => x.Id == command.DishId));

            if (ingredients.Count() != command.ExtraIngredientIds.Count)
            {
                throw new DishDoesNotContainIngredientException(command.DishId);
            }
            if (order == null)
            {
                throw new OrderNotFoundException(command.PhoneNumber, command.OrderNo);
            }
            if (dish == null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }

            var dishExtraIngredients = ingredients
                .Select(x => new DishExtraIngredient(x.Name, x.Price, command.Id))
                .ToList();

            var orderItem = new OrderItem(command.Id, command.OrderNo, dish, command.DishComment, dishExtraIngredients);
            order.AddOrderItem(orderItem);

            await _dishExtraIngredientRepository.AddManyAsync(dishExtraIngredients);
            await _orderItemRepository.AddAsync(orderItem);
            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();

            await _orderCalculationService.UpdateTotalPrice(order.Id);
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

        public async Task DeleteOrderItemAsync(System.Guid id)
        {
            var deletionResult = _orderItemRepository.RemoveOne(x => x.Id == id);

            if (deletionResult == false)
            {
                throw new NotFoundException(id, nameof(OrderItem));
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var allOrders = _orderRepository
                .GetAll()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderNo = x.OrderNo,
                    TotalPrice = x.TotalPrice,
                    Status = x.Status,
                    PaymentType = x.PaymentType,
                    ShippingAddress = x.ShippingAddress,
                    CustomerId = x.CustomerId,
                    Customer = x.Customer,
                    OrderItems = x.OrderItems.Select(x => new OrderItemDto
                    {
                        Id = x.Id,
                        DishName = x.DishName,
                        DishPrice = x.DishPrice,
                        DishComment = x.DishComment
                    })
                });

            return await allOrders.ToListAsync();
        }

        public async Task<OrdersListResponse> GetOrdersAsync(string phone, int orderNo)
        {
            var orders = _orderRepository
                .FindOrders(orderNo, phone);

            if (!orders.Any())
            {
                throw new OrderNotFoundException(phone, orderNo);
            }

            var ordersDto = orders.Select(x => new OrderDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                PaymentType = x.PaymentType,
                ShippingAddress = x.ShippingAddress,
                CustomerId = x.CustomerId,
                Customer = x.Customer,
                OrderItems = x.OrderItems.Select(x => new OrderItemDto
                {
                    Id = x.Id,
                    DishName = x.DishName,
                    DishPrice = x.DishPrice,
                    DishComment = x.DishComment
                })
            });

            return new OrdersListResponse(await ordersDto.ToListAsync());
        }

        public async Task AddOrderAddress(AddAddressCommand command)
        {
            var order = await _orderRepository
                .FindOneOrder(command.OrderNo, command.PhoneNumber);

            if (order is null)
            {
                throw new OrderNotFoundException(command.PhoneNumber, command.OrderNo);
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
            var order = await _orderRepository
                .FindOneOrder(command.OrderNo, command.Phone);

            order.SetPaymentMethod(command.PaymentType);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmOrder(AcceptOrderCommand command)
        {
            var order = await _orderRepository
                .FindOneOrder(command.OrderNo, command.PhoneNumber, x => x.OrderItems, x => x.ShippingAddress);

            if (!order.OrderItems.Any())
            {
                throw new Exception("Empty order");
            }
            if (order.ShippingAddress is null)
            {
                throw new Exception("Empty address");
            }
            if (order.PaymentType == Consts.Enums.PaymentType.NotSet)
            {
                throw new Exception("Empty payment method");
            }

            order.SetAsConfirmed();

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DinnerBillDto> GetDinnerBillAsync(int orderNo, string phone)
        {
            var order = await _orderRepository
               .FindMany(x => x.Customer.Phone == phone && x.OrderNo == orderNo)
               .Select(x => new DinnerBillDto
               {
                   OrderNo = orderNo,
                   Phone = phone,
                   Dishes = x.OrderItems.Select(orderItem => new DishBillDto
                   {
                       Name = orderItem.DishName,
                       BasePrice = orderItem.DishPrice,
                       Ingredients = orderItem.DishExtraIngredients.Select(extraIngredient => new IngredientBillDto
                       {
                           Name = extraIngredient.Name,
                           Price = extraIngredient.Price
                       })
                   }),
                   TotalPrice = x.TotalPrice
               })
               .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderNotFoundException(phone, orderNo);
            }

            return order;
        }
    }
}
