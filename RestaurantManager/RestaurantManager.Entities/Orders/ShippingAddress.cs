using System;

namespace RestaurantManager.Entities.Orders
{
    public class ShippingAddress : Entity
    {
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