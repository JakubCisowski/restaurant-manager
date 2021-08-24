using RestaurantManager.Consts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    public class IncorrectOrderStatus : Exception
    {
        public IncorrectOrderStatus() : base()
        {
        }

        public IncorrectOrderStatus(string message) : base(message)
        {
        }

        public IncorrectOrderStatus(int orderNo, OrderStatus orderStatus, OrderStatus expectedStatus)
            : this($"(Incorrect order status: {orderStatus} for order orderNo: {orderNo}. Valid order status for this operation is: {expectedStatus}")
        {
            OrderNo = orderNo;
            OrderStatus = orderStatus;
        }

        public int OrderNo { get; }
        public OrderStatus OrderStatus { get; }
    }
}
