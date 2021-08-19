using RestaurantManager.Entities.Orders;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderCalculationService : IOrderCalculationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGenericRepository<Order> _orderRepository;

        public OrderCalculationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.GetRepository<Order>();
        }

        public async Task UpdateTotalPrice(Guid id)
        {
            var orderItems = _orderRepository.FindMany(x => x.Id == id)
                .SelectMany(x => x.OrderItems);

            var dishPrices = orderItems.Sum(x => x.DishPrice);
            var ingredientsPrices = orderItems
                .Select(x => x.DishExtraIngredients.Sum(d => d.Price))
                .ToList()
                .Sum();

            var order = await _orderRepository.GetByIdAsync(id);
            order.SetTotalPrice(dishPrices + ingredientsPrices);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
