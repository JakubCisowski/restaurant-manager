using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Order
{
    public class Order : Entity
    {
        public float TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
