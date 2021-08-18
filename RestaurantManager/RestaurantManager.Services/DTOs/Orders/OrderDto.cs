using RestaurantManager.Consts.Enums;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Services.DTOs.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int OrderNo { get;  set; }
        public float TotalPrice { get;  set; }
        public OrderStatus Status { get;  set; }
        public PaymentType PaymentType { get;  set; }
        public ShippingAddress ShippingAddress { get;  set; }
        public Guid CustomerId { get;  set; }
        public Customer Customer { get;  set; }
        public IEnumerable<OrderItemDto> OrderItems { get;  set; }
    }

}
