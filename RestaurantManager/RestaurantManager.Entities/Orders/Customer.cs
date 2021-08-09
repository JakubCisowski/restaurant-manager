namespace RestaurantManager.Entities.Orders
{
    public class Customer : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }
}
