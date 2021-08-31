using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IAddressValidationService
    {
        Task<AddressValidationResult> Validate(Guid restaurantId, ShippingAddress address);
    }
}
