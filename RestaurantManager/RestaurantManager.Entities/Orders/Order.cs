using System.Collections.Generic;

namespace RestaurantManager.Entities.Orders
{
    public class Order : Entity
    {
        public float TotalPrice { get; private set; }
        public string Status { get; private set; }
        public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    }
}
