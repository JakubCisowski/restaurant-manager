using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Inputs.Orders
{
    public class AcceptPaymentInput
    {
        public string Phone { get; set; }
        public int OrderNo { get; set; }
    }
}
