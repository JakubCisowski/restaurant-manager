using System.Collections.Generic;

namespace RestaurantManager.Entities.Order
{
    public class Order : Entity
    {
        public float TotalPrice { get; private set; }
        public string Status { get; private set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
