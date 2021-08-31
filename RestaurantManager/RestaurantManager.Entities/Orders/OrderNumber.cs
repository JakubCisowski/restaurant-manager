using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Orders
{
    public class OrderNumber : Entity
    {
        public OrderNumber()
        {

        }

        public OrderNumber(int randomNo, Guid restaurantId)
        {
            OrderNo = randomNo;
            RestaurantId = restaurantId;
            InUsageFrom = DateTime.UtcNow;
        }

        public int OrderNo { get; private set; }
        public DateTime InUsageFrom { get; private set; }
        public Guid RestaurantId { get; private set; }

        public void ClearUsageFrom()
        {
            InUsageFrom = DateTime.Now;
        }
    }
}
