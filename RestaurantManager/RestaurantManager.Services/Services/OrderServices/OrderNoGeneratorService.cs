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
        private static IConfiguration _configuration;
        private static RestaurantDbContext _dbContext;
        private static Random rand = new Random();

        public OrderNoGeneratorService(RestaurantDbContext dbContext, IConfiguration iConfig, IGeneratorLockService generatorLock)
        {
            _dbContext = dbContext;
            _configuration = iConfig;
            _generatorLock = generatorLock;
        }

        public int GenerateOrderNo()
        {
            // Doszedłem do wniosku że trzeba zlockować całą tę metodę - wyjaśnienie wewnątrz
            lock(_generatorLock)
            {
                return _generatorLock.GenerateOrderNo();
            }
        }

        public static int CreateNewOrderNumber()
        {
            var randomNo = rand.Next(0, 1000000);
            var exitsts = _dbContext.OrderNumbers
                .Any(x => x.Id == randomNo);

            if (!exitsts)
            {
                CreateOrderNumberRecord(randomNo);
                return randomNo;
            }
            else
            {
                return CreateNewOrderNumber();
            }
        }

        private static bool CheckIfOrderNumberExipred(int timeDifference)
        {
            int EXPIRATION_TIME_IN_DAYS = Int32.Parse(_configuration.GetSection("Consts").GetSection("OrderNoExpirationTimeInDays").Value);
            return timeDifference > EXPIRATION_TIME_IN_DAYS;
        }

        private static int GetUsageTimeDays(DateTime inUsageSince)
        {
            var currentDate = DateTime.Now;
            var timeDifference = (int)currentDate.Subtract(inUsageSince).TotalDays;
            return timeDifference;
        }

        public static OrderNumber GetNumberFromExpiredRecords()
        {
            var numberRecord = _dbContext
                .OrderNumbers
                .OrderByDescending(x => x.InUsageFrom)
                .FirstOrDefault();

            if (numberRecord is null)
            {
                return null;
            }

            int timeDifference = GetUsageTimeDays(numberRecord.InUsageFrom);
            if (CheckIfOrderNumberExipred(timeDifference))
            {
                return numberRecord;
            }
            else
            {
                return null;
            }
        }

        private static void CreateOrderNumberRecord(int randomNo)
        {
            _dbContext.OrderNumbers.Add(new OrderNumber()
            {
                Id = randomNo,
                InUsageFrom = DateTime.Now,
            });

            _dbContext.SaveChanges();
        }
    }
}
