using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.ErrorResponses
{
    public class OrderErrorResponse
    {

        public OrderErrorResponse(int orderNo ,string phone, string message)
        {
            PhoneNumber = phone;
            ExceptionMessage = message;
            OrderNo = orderNo;
        }

        public string PhoneNumber { get; set; }
        public string ExceptionMessage { get; set; }
        public int OrderNo { get; }
    }
}
