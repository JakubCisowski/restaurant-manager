using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Inputs.Orders
{
    public class CreateOrderInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; } //co ma być w adresie? i czy ma w ogóle być
    }
}
