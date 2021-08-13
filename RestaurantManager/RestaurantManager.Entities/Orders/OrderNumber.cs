using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Orders
{
    //seed new 5 000 000 records
    public class OrderNumber
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime InUsageFrom { get; set; }
        // older than 2 months
    }
}
