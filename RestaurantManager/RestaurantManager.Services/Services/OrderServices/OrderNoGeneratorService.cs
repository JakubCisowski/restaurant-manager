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
        private readonly IGeneratorLockService _generatorLock;

        public OrderNoGeneratorService(IGeneratorLockService generatorLock)
        {
            _generatorLock = generatorLock;
        }

        public int GenerateOrderNo()
        {
            return _generatorLock.GenerateOrderNo();
        }
    }
}
