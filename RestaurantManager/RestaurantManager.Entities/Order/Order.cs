using System.Collections.Generic;

namespace RestaurantManager.Entities.Order
{
    public class Order : Entity
    {
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
