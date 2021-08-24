using RestaurantManager.Consts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class SetPaymentMethodCommand
    {
        public int OrderNo { get; set; }
        public string Phone { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
