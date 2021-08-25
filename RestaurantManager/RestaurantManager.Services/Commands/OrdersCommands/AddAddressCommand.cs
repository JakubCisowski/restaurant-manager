using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class AddAddressCommand
    {
        public int OrderNo { get; set; }
        public string CustomerPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
