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
        private readonly IUnitOfWork _unitOfWork;

        public OrderNoGeneratorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GenerateOrderNo()
        {
            Random rand = new Random();
            //1. sprawdzasz czy w tabeli OrderNumber instnieje id rand
            //2. Jeśli nie istnieje to tworzysz rekord o tym id
            //3. Jeśli istnieje to sprawdzasz czy dostępny
            //4. Jeśli nie to sprawdzasz czy można na odblokować
            //5. Jeśli nie to następny wolny
            // zapytać dominika co robić z przedawnionymi idkami
            return 0;
        }
    }
}
