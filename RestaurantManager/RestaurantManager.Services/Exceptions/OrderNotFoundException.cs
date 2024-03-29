﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string phone, int orderNo) : this($"Not found order for declared request phone: {phone}, orderNo: {orderNo}")
        {
            Phone = phone;
            OrderNo = orderNo;
        }

        public OrderNotFoundException(Guid restaurantId) : this($"No orders found for declared restaurant id: {restaurantId}")
        {
            RestaurantId = restaurantId;
        }

        public string Phone { get; }
        public int OrderNo { get; }
        public Guid RestaurantId { get; }
    }
}
