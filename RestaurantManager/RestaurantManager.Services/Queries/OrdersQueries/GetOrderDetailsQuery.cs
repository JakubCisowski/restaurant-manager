using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.OrdersQueries
{
    public class GetOrderDetailsQuery
    {
        public GetOrderDetailsQuery(string phone, int orderNo)
        {
            Phone = phone;
            OrderNo = orderNo;
        }

        public string Phone { get; }
        public int OrderNo { get; }
    }
}
