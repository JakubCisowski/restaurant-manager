using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.Orders
{
    public class ShippingAddressDto
    {
        public ShippingAddressDto(ShippingAddress shippingAddress)
        {
            Country = shippingAddress?.Country;
            City = shippingAddress?.City;
            Address1 = shippingAddress?.Address1;
            Address2 = shippingAddress?.Address2;
            ZipPostalCode = shippingAddress?.ZipPostalCode;
            PhoneNumber = shippingAddress?.PhoneNumber;
        }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
