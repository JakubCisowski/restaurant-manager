using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Inputs.Orders
{
    public class AcceptPaymentInput
    {
        public int OrderNo { get; set; }
        public string Status { get; set; }
    }
}
