using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderCalculationService
    {
        Task UpdateTotalPrice(Guid id);
    }
}
