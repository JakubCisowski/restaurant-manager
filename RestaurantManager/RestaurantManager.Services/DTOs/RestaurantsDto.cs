using System;

namespace RestaurantManager.Services.DTOs
{
    public class RestaurantsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
