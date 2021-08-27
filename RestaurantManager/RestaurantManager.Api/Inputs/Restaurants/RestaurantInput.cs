using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.Api.Inputs.Restaurants
{
    public class RestaurantInput
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public RestaurantAddress Address { get; set; }
    }
}
