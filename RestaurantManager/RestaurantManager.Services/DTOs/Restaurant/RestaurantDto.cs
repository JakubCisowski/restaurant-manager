using System;

namespace RestaurantManager.Services.DTOs
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Guid? MenuId { get; set; }
    }
}
