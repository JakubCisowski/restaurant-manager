using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.Commands.RestaurantCommands.Restaurants;

namespace RestaurantManager.Api.Inputs.Restaurants
{
    public class RestaurantInput
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public double MaxShippingDistanceRadius { get; set; }


    }
}
