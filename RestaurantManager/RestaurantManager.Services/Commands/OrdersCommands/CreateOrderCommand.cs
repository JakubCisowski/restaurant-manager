﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class CreateOrderCommand
    {
        public Guid RestaurantId { get; set; }
        public string Phone { get; set; }
    }
}
