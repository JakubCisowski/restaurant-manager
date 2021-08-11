using System;

namespace RestaurantManager.Api.Inputs.Restaurants
{
    public class DishInput
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public Guid MenuId { get; set; }
    }
}
