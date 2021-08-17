using RestaurantManager.Context;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Linq;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderNoGeneratorService : IOrderNoGeneratorService
    {
        private const int EXPIRATION_TIME_IN_DAYS = 30;

        private readonly RestaurantDbContext _dbContext;
        private static Random rand = new Random();

        public OrderNoGeneratorService(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GenerateOrderNo()
        {
            var availableNumber = GetNumberFromExpiredRecords();
            if (availableNumber is not null)
            {
                availableNumber.ClearUsageFrom();
                return availableNumber.Id;
            }

            return CreateNewOrderNumber();
        }

        private int CreateNewOrderNumber()
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
            return timeDifference > EXPIRATION_TIME_IN_DAYS;
        }

        private static int GetUsageTimeDays(DateTime inUsageSince)
        {
            var currentDate = DateTime.Now;
            var timeDifference = (int)currentDate.Subtract(inUsageSince).TotalDays;
            return timeDifference;
        }

        private OrderNumber GetNumberFromExpiredRecords()
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

        private void CreateOrderNumberRecord(int randomNo)
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
