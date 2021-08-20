using RestaurantManager.Entities.Orders;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using RestaurantManager.Services.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Context;
using Microsoft.Extensions.Configuration;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class GeneratorLockService : IGeneratorLockService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly Random rand = new Random();

        public GeneratorLockService(RestaurantDbContext dbContext, IConfiguration iConfig)
        {
            _configuration = iConfig;
            _dbContext = dbContext;
        }

        public OrderNumber GetOldestAvailableNumberRecord()
        {
            lock (LockFactory.LockObject)
            {
                var oldestNumberRecord = _dbContext
                        .OrderNumbers
                        .OrderByDescending(x => x.InUsageFrom)
                        .FirstOrDefault();

                if (oldestNumberRecord is null)
                {
                    return null;
                }

                int timeDifference = GetUsageTimeDays(oldestNumberRecord.InUsageFrom);
                if (IsOrderNumberExipred(timeDifference))
                {
                    oldestNumberRecord.ClearUsageFrom();
                    _dbContext.OrderNumbers.Update(oldestNumberRecord);
                    _dbContext.SaveChanges();
                    return oldestNumberRecord;
                }
                else
                {
                    return null;
                }
            }
        }

        public int GenerateNewOrderNumberRecord()
        {
            lock (LockFactory.LockObject)
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
                    return GenerateNewOrderNumberRecord();
                }
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

        private int GetUsageTimeDays(DateTime inUsageSince)
        {
            var currentDate = DateTime.Now;
            var timeDifference = (int)currentDate.Subtract(inUsageSince).TotalDays;
            return timeDifference;
        }

        private bool IsOrderNumberExipred(int timeDifference)
        {
            int EXPIRATION_TIME_IN_DAYS = Int32.Parse(_configuration.GetSection("Consts").GetSection("OrderNoExpirationTimeInDays").Value);
            return timeDifference > EXPIRATION_TIME_IN_DAYS;
        }
    }
}
