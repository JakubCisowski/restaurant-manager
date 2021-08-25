using System;

namespace RestaurantManager.Entities.Orders
{
    public class ShippingAddress : Entity
    {
        public ShippingAddress(string country, string city, string address1, string address2, string phoneNumber, string zipPostalCode, Guid orderId)
        {
            Country = country;
            City = city;
            Address1 = address1;
            Address2 = address2;
            PhoneNumber = phoneNumber;
            ZipPostalCode = zipPostalCode;
            OrderId = orderId;
        }

        public string Country { get; private set; }
        public string City { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipPostalCode { get; private set; }
        public string PhoneNumber { get; private set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}