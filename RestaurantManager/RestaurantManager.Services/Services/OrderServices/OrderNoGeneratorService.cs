using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class OrderNoGeneratorService : IOrderNoGeneratorService
    {
        private const int EXPIRATION_TIME_IN_DAYS = 30;

        private readonly DbSet<OrderNumber> _orderNumbersSet;
        private readonly IUnitOfWork _unitOfWork;

        public OrderNoGeneratorService(IUnitOfWork unitOfWork, DbSet<OrderNumber> menuSet)
        {
            _unitOfWork = unitOfWork;
            _orderNumbersSet = menuSet;
        }

        public int GenerateOrderNo()
        {
            // Skoro ta metoda ma tylko generować liczbę, to po co nam właściwie unitOfWork?

            // Jesli ma dodawac też do bazy, to wtedy wszystkie poniższe 'break;' należy podmienić z:
            //_orderNumbersSet.Add(new OrderNumber()
            //{
            //    Id = randomNo,
            //    InUsageFrom = DateTime.Now,
            //    IsAvailable = false  
            //});
            // _unitOfWork.SaveChangesAsync();

            // zapytać dominika co robić z przedawnionymi idkami



            var rand = new Random();
            int randomNo = rand.Next(0, 1000000); // Random value <0 - 999 999>

            while(true)
            {
                //1. sprawdzasz czy w tabeli OrderNumber instnieje id rand
                var selectedNo = _orderNumbersSet.FirstOrDefault(x => x.Id == randomNo);

                //2. Jeśli nie istnieje to tworzysz rekord o tym id
                if (selectedNo == null)
                {
                    break;
                }
                //3. Jeśli istnieje to sprawdzasz czy dostępny
                else
                {
                    //4. Jeśli niedostępny to sprawdzasz czy można na odblokować
                    if (selectedNo.IsAvailable == false)
                    {
                        var inUsageSince = selectedNo.InUsageFrom;
                        var currentDate = DateTime.Now;
                        var timeDifference = (int)currentDate.Subtract(inUsageSince).TotalDays;

                        if(timeDifference > EXPIRATION_TIME_IN_DAYS)
                        {
                            break;
                        }
                        //5. Jeśli nie to sprawdzasz inny
                        else
                        {
                            randomNo = rand.Next(0, 1000000);
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return randomNo;
        }
    }
}
