using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Order
{
    public class ShippingMethod : Entity
    {
        public string Address { get; set; }
        public string Type { get; set; }
    }
}
