using RestaurantManager.Consts.Enums;
using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Orders
{
    public class Order : Entity
    {
        public int OrderNo { get; private set; }
        public float TotalPrice { get; private set; }
        public OrderStatus Status { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public ShippingAddress ShippingAddress { get; private set; }

        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    }
    //pobieranie zamówienia po nr telefonu, id zamówienia => int 6 cyfr, archiwizacja nr zamówienia po jakimś czasie wygasa
}
