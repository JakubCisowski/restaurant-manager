using Microsoft.EntityFrameworkCore;
using RestaurantManager.Consts.Enums;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Commands.Orders;
using RestaurantManager.Services.Commands.OrdersCommands;
using RestaurantManager.Services.DTOs.Ingredients;
using RestaurantManager.Services.DTOs.OrderItems;
using RestaurantManager.Services.DTOs.Orders;
using RestaurantManager.Services.Exceptions;
using RestaurantManager.Services.Queries.OrdersQueries;
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

        public OrderService(IUnitOfWork unitOfWork,
                            ICustomerService customerService,
                            IOrderNoGeneratorService orderNoGeneratorService,
                            IOrderCalculationService orderCalculationService)
        {
            _unitOfWork = unitOfWork;
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
            var order = await _unitOfWork.OrderRepository
                .FindOneOrder(command.OrderNo, command.PhoneNumber);

            if (order == null)
            {
                throw new OrderNotFoundException(command.PhoneNumber, command.OrderNo);
            }

            var dish = await _dishRepository
                .FindOneAsync(x => x.Id == command.DishId && x.Menu.RestaurantId == order.RestaurantId);

            var ingredients = _ingredientRepository
                .FindMany(x => command.ExtraIngredientIds.Contains(x.Id)
                          && x.Dishes.Any(x => x.Id == command.DishId));

            if (ingredients.Count() != command.ExtraIngredientIds.Distinct().Count())
            {
                throw new DishDoesNotContainIngredientException(command.DishId);
            }
            if (dish == null)
            {
                throw new NotFoundException(command.DishId, nameof(Dish));
            }
            if (order.Status != OrderStatus.New)
            {
                throw new IncorrectOrderStatus(order.OrderNo, order.Status, OrderStatus.New);
            }

            var dishExtraIngredients = command.ExtraIngredientIds
                .Select(x =>
                {
                    var ingredient = ingredients.FirstOrDefault(i => i.Id == x);
                    return new DishExtraIngredient(ingredient.Name, ingredient.Price, command.OrderItemId);
                }).ToList();

            var orderItem = new OrderItem(command.OrderItemId, order.Id, command.OrderNo, dish, command.DishComment, dishExtraIngredients);
            order.AddOrderItem(orderItem);

            await _dishExtraIngredientRepository.AddManyAsync(dishExtraIngredients);
            await _orderItemRepository.AddAsync(orderItem);

            _unitOfWork.OrderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
            await _orderCalculationService.UpdateTotalPrice(order.Id);
        }

        public async Task<int> CreateOrderDraft(CreateOrderCommand command)
        {
            var order = new Order();
            order.SetCustomer(await GetCustomer(command.Phone));
            order.SetOrderNumber(_orderNoGeneratorService.GenerateOrderNo(command.RestaurantId));
            order.SetRestaurant(command.RestaurantId);

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order.OrderNo;
        }

        public async Task DeleteOrderItemAsync(DeleteOrderItemCommand command)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(command.Id);
            if (order?.Status != OrderStatus.New)
            {
                throw new IncorrectOrderStatus(order.OrderNo, order.Status, OrderStatus.New);
            }

            var deletionResult = _orderItemRepository.RemoveOne(x => x.Id == command.Id);

            if (deletionResult == false)
            {
                throw new NotFoundException(command.Id, nameof(OrderItem));
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var allOrders = _unitOfWork.OrderRepository
                .GetAll()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderNo = x.OrderNo,
                    TotalPrice = x.TotalPrice,
                    Status = x.Status,
                    PaymentType = x.PaymentType,
                    ShippingAddress = new ShippingAddressDto(x.ShippingAddress),
                    CustomerPhone = x.Customer.Phone,
                    OrderItems = x.OrderItems.Select(x => new OrderSimpleItemDto
                    {
                        Id = x.Id,
                        DishName = x.DishName,
                        DishPrice = x.DishPrice,
                        DishComment = x.DishComment
                    })
                });

            return await allOrders.ToListAsync();
        }

        public async Task<OrderDetailsDto> GetOrderDetailsAsync(GetOrderDetailsQuery query)
        {
            var orders = _unitOfWork.OrderRepository
                .FindOrders(query.OrderNo, query.Phone);

            if (!orders.Any())
            {
                throw new OrderNotFoundException(query.Phone, query.OrderNo);
            }

            var orderDto = await orders.Select(x => new OrderDetailsDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                PaymentType = x.PaymentType,
                ShippingAddress = new ShippingAddressDto(x.ShippingAddress),
                CustomerPhone = x.Customer.Phone,
                OrderItems = x.OrderItems.Select(x => new OrderItemDto
                {
                    Id = x.Id,
                    DishName = x.DishName,
                    DishPrice = x.DishPrice,
                    DishComment = x.DishComment,
                    DishExtraIngredients = x.DishExtraIngredients.Select(i => new IngredientBaseDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Price = i.Price
                    })
                })
            }).FirstOrDefaultAsync();

            return orderDto;
        }

        public async Task AddOrderAddress(AddAddressCommand command)
        {
            var order = await _unitOfWork.OrderRepository
                .FindOneOrder(command.OrderNo, command.CustomerPhone);

            if (order is null)
            {
                throw new OrderNotFoundException(command.CustomerPhone, command.OrderNo);
            }
            if (order.Status != OrderStatus.New)
            {
                throw new IncorrectOrderStatus(order.OrderNo, order.Status, OrderStatus.New);
            }

            var address = new ShippingAddress(command.Country, command.City, command.Address1, command.Address2, command.PhoneNumber, command.ZipPostalCode, order.Id);
            await _addressRepository.AddAsync(address);

            order.SetAddress(address);
            _unitOfWork.OrderRepository.Update(order);

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
            var order = await _unitOfWork.OrderRepository
                .FindOneOrder(command.OrderNo, command.Phone);

            if (order is null)
            {
                throw new OrderNotFoundException(command.Phone, command.OrderNo);
            }
            if (order.Status != OrderStatus.New)
            {
                throw new IncorrectOrderStatus(order.OrderNo, order.Status, OrderStatus.New);
            }
            order.SetPaymentMethod(command.PaymentType);

            _unitOfWork.OrderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmOrder(AcceptOrderCommand command)
        {
            var order = await _unitOfWork.OrderRepository
                .FindOneOrder(command.OrderNo, command.PhoneNumber, x => x.OrderItems, x => x.ShippingAddress);

            if (!order.OrderItems.Any())
            {
                throw new NotFoundException("Empty order");
            }
            if (order.ShippingAddress is null)
            {
                throw new NotFoundException("Empty address");
            }
            if (order.PaymentType == Consts.Enums.PaymentType.NotSet)
            {
                throw new PaymentTypeNotSetException(order.OrderNo, order.PaymentType);
            }

            order.SetAsConfirmed();

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DinnerBillDto> GetDinnerBillAsync(GetDinnerBillQuery query)
        {
            var order = await _unitOfWork.OrderRepository
               .FindMany(x => x.Customer.Phone == query.Phone && x.OrderNo == query.OrderNo)
               .Select(x => new DinnerBillDto
               {
                   OrderNo = query.OrderNo,
                   Phone = query.Phone,
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
                throw new OrderNotFoundException(query.Phone, query.OrderNo);
            }

            return order;
        }

        public async Task AcceptPaymentAsync(AcceptPaymentCommand command)
        {
            var order = await _unitOfWork.OrderRepository
                .FindOneOrder(command.OrderNo, command.Phone);

            if (order == null)
            {
                throw new OrderNotFoundException(command.Phone, command.OrderNo);
            }

            if (order.Status < OrderStatus.Paid)
            {
                order.SetStatus(OrderStatus.Paid);
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new IncorrectOrderStatus(command.OrderNo, order.Status, OrderStatus.Confirmed);
            }
        }

        public async Task<OrdersListResponse> CustomerOrders(GetCustomerOrdersQuery query)
        {
            var orders = _unitOfWork.OrderRepository
                .FindMany(x => x.Customer.Phone == query.Phone);

            if (!orders.Any())
            {
                throw new OrderNotFoundException(query.Phone, 0);
            }

            var orderDto = await orders.Select(x => new OrderDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                PaymentType = x.PaymentType,
                ShippingAddress = new ShippingAddressDto(x.ShippingAddress),
                CustomerPhone = x.Customer.Phone,
                OrderItems = x.OrderItems.Select(x => new OrderSimpleItemDto
                {
                    Id = x.Id,
                    DishName = x.DishName,
                    DishPrice = x.DishPrice,
                    DishComment = x.DishComment
                })
            }).ToListAsync();

            return new OrdersListResponse(orderDto);
        }

        public async Task<OrdersListResponse> RestaurantOrders(GetRestaurantOrdersQuery query)
        {
            var orders = _unitOfWork.OrderRepository
                .FindMany(x => x.RestaurantId == query.RestaurantId);

            if (!orders.Any())
            {
                throw new OrderNotFoundException(query.RestaurantId);
            }

            var orderDto = await orders.Select(x => new OrderDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                PaymentType = x.PaymentType,
                ShippingAddress = new ShippingAddressDto(x.ShippingAddress),
                CustomerPhone = x.Customer.Phone,
                OrderItems = x.OrderItems.Select(x => new OrderSimpleItemDto
                {
                    Id = x.Id,
                    DishName = x.DishName,
                    DishPrice = x.DishPrice,
                    DishComment = x.DishComment
                })
            }).ToListAsync();

            return new OrdersListResponse(orderDto);
        }
    }
}
