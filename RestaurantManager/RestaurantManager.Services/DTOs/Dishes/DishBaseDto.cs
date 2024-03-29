﻿using System;

namespace RestaurantManager.Services.DTOs.Dishes
{
    public class DishBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public Guid? MenuId { get; set; }
    }
}
