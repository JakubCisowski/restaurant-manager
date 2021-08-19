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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<DishExtraIngredient> _dishExtraIngredientRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IOrderNoGeneratorService _orderNoGeneratorService;

        public OrderService(IUnitOfWork unitOfWork,
                            ICustomerService customerService,
                            IOrderNoGeneratorService orderNoGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.GetRepository<Order>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _dishRepository = _unitOfWork.GetRepository<Dish>();
            _ingredientRepository = _unitOfWork.GetRepository<Ingredient>();
            _dishExtraIngredientRepository = _unitOfWork.GetRepository<DishExtraIngredient>();
            _customerRepository = _unitOfWork.GetRepository<Customer>();
            _customerService = customerService;
            _orderNoGeneratorService = orderNoGeneratorService;
        }

        public async Task AddOrderItemAsync(AddOrderItemCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);
            var dish = await _dishRepository.GetByIdAsync(command.DishId);
            var ingredients = _ingredientRepository.FindMany(x => command.ExtraIngredientIds.Contains(x.Id));

            var areExtraIngredientsAvailable = _dishRepository
                .FindMany(x => x.Id == command.DishId &&
                    command.ExtraIngredientIds.All(i => x.Ingredients.Any(ingredient => ingredient.Id == i))).Any();

            if (!areExtraIngredientsAvailable)
            {
                throw new DishDoesNotContainIngredientException(command.DishId);
            }
            if (order == null)
            {
                throw new NotFoundException(command.OrderId, nameof(Order));
            }
            if (dish == null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }

            var dishExtraIngredients = ingredients
                .Select(x => new DishExtraIngredient(x.Name, x.Price, command.Id))
                .ToList();

            var orderItem = new OrderItem(command.Id, command.OrderId, dish, command.DishComment, dishExtraIngredients);
            order.AddOrderItem(orderItem);

            await _dishExtraIngredientRepository.AddManyAsync(dishExtraIngredients);
            await _orderItemRepository.AddAsync(orderItem);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task<OrdersListResponse> GetOrdersAsync(string phone)
        {
            var orders = _orderRepository
                .FindMany(x => x.Customer.Phone == phone);

            if(!orders.Any())
            {
                throw new NotFoundException(phone, nameof(Order));
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

        private async Task<Customer> GetCustomer(string phone)
        {
            var customer = await _customerService.GetCustomer(phone);

            if (customer is null)
            {
                customer = await _customerService.CreateCustomer(phone);
            }

            return customer;
        }
    }
}
