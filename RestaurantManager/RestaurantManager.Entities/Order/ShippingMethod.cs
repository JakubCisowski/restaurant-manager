namespace RestaurantManager.Entities.Order
{
    public class ShippingMethod : Entity
    {
        public string Address { get; private set; }
        public string Type { get; private set; }
    }
}
