using RestaurantManager.Entities.Restaurants;
using System;

namespace RestaurantManager.Services.Commands.Restaurants
{
    public class CreateRestaurantCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public RestaurantAddress Address { get; set; }

        public CreateRestaurantCommand(Guid restaurantId, string name, string phone, RestaurantAddress address)
        {
            Id = restaurantId;
            Name = name;
            Phone = phone;
            Address = address;
        }
    }
}
