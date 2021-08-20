using RestaurantManager.Entities.Orders;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using RestaurantManager.Services.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class GeneratorLockService : IGeneratorLockService
    {
        public int GenerateOrderNo()
        {
            OrderNumber availableNumber;

            // Tę linijkę trzeba zlockować
            // np. Dwóm osobom wygeneruje ten sam najstarszy numer i jesli jest dostepny do archiwizacji to stworzy dwa takie same
            availableNumber = OrderNoGeneratorService.GetNumberFromExpiredRecords();

            // Ten if też trzeba zlockować
            // Bo nawet jeśli powyższa metoda jest zlockowana, to i tak druga osoba może wylosować ten sam 'availableNumber' zanim ten if się wykona.
            if (availableNumber is not null)
            {
                availableNumber.ClearUsageFrom();
                return availableNumber.Id;
            }

            // A to również trzeba zlockować
            // Bo nawet jak powyższe są zlockowane to wejdzie dwóm osobom do tej metody i w tym samym momencie wejdzie im do pierwszego ifa tam.
            // Tym samym tworząc dwa takie same numery
            return OrderNoGeneratorService.CreateNewOrderNumber();\
        }
    }
}
