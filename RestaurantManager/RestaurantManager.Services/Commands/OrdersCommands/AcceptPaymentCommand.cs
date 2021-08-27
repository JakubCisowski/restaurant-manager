using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class AcceptPaymentCommand
    {
        public AcceptPaymentCommand(int orderNo, string phone)
        {
            OrderNo = orderNo;
            Phone = phone;
        }

        public int OrderNo { get; }
        public string Phone { get; }
    }
}
