using System;

namespace RestaurantManager.Entities.Orders
{
    public class OrderNumber
    {
        public int Id { get; set; }
        public DateTime InUsageFrom { get; set; }

        public void ClearUsageFrom()
        {
            InUsageFrom = DateTime.Now;
        }
        // older than 2 months
    }
}
