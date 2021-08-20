using Microsoft.Extensions.Configuration;
using RestaurantManager.Context;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Linq;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderNoGeneratorService : IOrderNoGeneratorService
    {
        private readonly IGeneratorLockService _lockService;

        public OrderNoGeneratorService(IGeneratorLockService generatorLock)
        {
            _lockService = generatorLock;
        }

        public int GenerateOrderNo()
        {
            var oldestAvailableRecord = _lockService.GetOldestAvailableNumberRecord();

            if (oldestAvailableRecord is not null)
            {
                return oldestAvailableRecord.Id;
            }

            return _lockService.GenerateNewOrderNumberRecord();
        }
    }
}
