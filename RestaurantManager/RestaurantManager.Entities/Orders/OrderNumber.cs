using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Orders
{
    public class OrderNumber
    {
        public OrderNumber()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.None), Key]
        public int Id { get; set; }
        public DateTime InUsageFrom { get; set; }

        public void ClearUsageFrom()
        {
            InUsageFrom = DateTime.Now;
        }
        // older than 2 months
    }
}
