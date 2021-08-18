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
            _customerService = customerService;
            _orderNoGeneratorService = orderNoGeneratorService;
        }

        public async Task AddOrderItemAsync(AddOrderItemCommand newOrderItem)
        {
            var order = await _orderRepository.GetByIdAsync(newOrderItem.OrderId);
            var dish = await _dishRepository.GetByIdAsync(newOrderItem.DishId);
            var ingredients = _ingredientRepository.FindMany(x => newOrderItem.ExtraIngredientIds.Contains(x.Id));

            //if(!dish.Ingredients.Select(x=> x.Id).Contains(newOrderItem.ExtraIngredientIds))
            //{
            //    throw new System.Exception($"Error while adding order item to order (id={newOrderItem.OrderId} - one of extra ingredients is not available for dish (id={newOrderItem.DishId}).");
            //}
            if (order == null)
            {
                throw new NotFoundException(newOrderItem.OrderId, nameof(Order));
            }
            if (dish == null)
            {
                throw new NotFoundException(newOrderItem.DishId, nameof(Dish));
            }
            if(ingredients.Count() != newOrderItem.ExtraIngredientIds.Count)
            {
                var notFoundIngredientId = newOrderItem.ExtraIngredientIds.Where(x => !(ingredients.Select(y=> y.Id).Contains(x))).FirstOrDefault();
                throw new NotFoundException(notFoundIngredientId, nameof(Ingredient));
            }

            var dishExtraIngredients = new List<DishExtraIngredient>();
            ingredients.ToList().ForEach(x => dishExtraIngredients.Add(new DishExtraIngredient(x.Name, x.Price, newOrderItem.Id)));

            var orderItem = new OrderItem(newOrderItem.Id, newOrderItem.OrderId, dish, newOrderItem.DishComment, dishExtraIngredients);
            orderItem.SetOrder(order);

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
                    OrderItems = (ICollection<OrderItemDto>)x.OrderItems.Select(x => new OrderItemDto // nie wiem jak inaczej niż explicit castem
                    {
                        Id = x.Id,
                        DishName = x.DishName,
                        DishPrice = x.DishPrice,
                        DishComment = x.DishComment
                    })
                });

            return await allOrders.ToListAsync();
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
