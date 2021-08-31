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

        public string Country { get; private set; }
        public string City { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipPostalCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Guid RestaurantId { get; private set; }
        public virtual Restaurant Restaurant { get; private set; }

        public void SetLatLong(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
