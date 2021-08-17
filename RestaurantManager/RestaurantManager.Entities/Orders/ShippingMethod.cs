namespace RestaurantManager.Entities.Orders
{
    public class ShippingMethod : Entity //bez tego narazie
    {
        public string Address { get; private set; }
        public string Type { get; private set; }
    }
}
