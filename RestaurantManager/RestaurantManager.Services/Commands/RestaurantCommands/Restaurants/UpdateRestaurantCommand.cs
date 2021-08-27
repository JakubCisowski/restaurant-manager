using RestaurantManager.Entities.Restaurants;
using System;

namespace RestaurantManager.Services.Commands.Restaurants
{
    public class UpdateRestaurantCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public RestaurantAddress Address { get; set; }
    }
}
