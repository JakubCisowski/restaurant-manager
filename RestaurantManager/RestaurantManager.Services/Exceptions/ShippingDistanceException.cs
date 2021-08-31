using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    public class ShippingDistanceException : Exception
    {
        public ShippingDistanceException() : base()
        {
        }

        public ShippingDistanceException(string message) : base(message)
        {
        }

        public ShippingDistanceException(double distance, double maxDistance) 
            : this($"Incorrect shipping address, maximum allowed distance is: {maxDistance} meters, your destination address is {distance} meters from our restaurant.")
        {
        }
    }
}
