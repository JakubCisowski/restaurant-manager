using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.Restaurants
{
    public class CreateRestaurantCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CreateRestaurantCommand(Guid restaurantId, string name, string phone, string address)
        {
            Id = restaurantId;
            Name = name;
            Phone = phone;
            Address = address;
        }
    }
}
