using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Services.Commands.RestaurantCommands.Restaurants;
using System;

namespace RestaurantManager.Services.Commands.Restaurants
{
    public class CreateRestaurantCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public double MaxShippingDistanceRadius { get; }

        public CreateRestaurantCommand(Guid restaurantId, string name, string phone, Address address, double maxShippingDistanceRadius)
        {
            Id = restaurantId;
            Name = name;
            Phone = phone;
            Address = address;
            MaxShippingDistanceRadius = maxShippingDistanceRadius;
        }
    }
}
