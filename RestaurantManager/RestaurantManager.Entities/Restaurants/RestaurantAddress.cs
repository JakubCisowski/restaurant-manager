using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Restaurants
{
    public class RestaurantAddress : Entity
    {
        public RestaurantAddress(string country, string city, string address1, string address2, string phoneNumber, string zipPostalCode, Guid restaurantId)
        {
            Country = country;
            City = city;
            Address1 = address1;
            Address2 = address2;
            PhoneNumber = phoneNumber;
            ZipPostalCode = zipPostalCode;
            RestaurantId = restaurantId;
        }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get;  set; }

        public Guid RestaurantId { get; private set; }
        private Restaurant Restaurant { get; set; }

    }
}
