using RestaurantManager.Services.DTOs.Restaurant;
using System;

namespace RestaurantManager.Services.DTOs
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public RestaurantAddressDto Address { get; set; } 
        public Guid? MenuId { get; set; }
    }
}
