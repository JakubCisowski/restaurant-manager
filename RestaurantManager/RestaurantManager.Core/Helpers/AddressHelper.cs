using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Helpers
{
    public static class AddressHelper
    {
        public static string GetAddressString(string city, string address1, string address2)
        {
            return string.Join(" ", city, address1, address2);
        }
    }
}
