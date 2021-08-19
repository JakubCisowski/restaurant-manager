using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class AcceptOrderCommand
    {
        public string PhoneNumber { get; set; }
        public int OrderNo { get; set; }
    }
}
