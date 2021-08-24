using System.Collections.Generic;

namespace RestaurantManager.Entities.Orders
{
    public class Customer : Entity
    {
        public Customer()
        {

        }
        public Customer(string phone)
        {
            Phone = phone;
        }

        public string Phone { get; private set; }

        public virtual ICollection<Order> Orders { get; private set; }
    }
}
