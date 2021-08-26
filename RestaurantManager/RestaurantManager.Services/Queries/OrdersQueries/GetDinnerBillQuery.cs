using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.OrdersQueries
{
    public class GetDinnerBillQuery
    {
        public GetDinnerBillQuery(int orderNo, string phone)
        {
            OrderNo = orderNo;
            Phone = phone;
        }

        public int OrderNo { get; }
        public string Phone { get; }
    }
}
