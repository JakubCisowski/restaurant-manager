using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.OrdersQueries
{
    public class GetCustomerOrdersQuery
    {
        public string Phone { get; }

        public GetCustomerOrdersQuery(string phone)
        {
            Phone = phone;
        }
    }
}
