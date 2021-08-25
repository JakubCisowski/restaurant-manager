using RestaurantManager.Consts.Enums;
using RestaurantManager.Services.DTOs.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.Orders
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public string CustomerPhone { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
