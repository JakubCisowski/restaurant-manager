﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class RemoveOrderItemCommand
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
    }

}
