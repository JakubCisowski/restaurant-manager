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

        public void SetCustomer(Customer customer)
        {
            Customer = customer;
        }

        public void SetOrderNumber(int orderNo)
        {
            OrderNo = orderNo;
        }

        public void SetAddress(ShippingAddress address)
        {
            ShippingAddress = address;
        }

        public void SetPaymentMethod(PaymentType paymentType)
        {
            PaymentType = paymentType;
        }

        public void SetAsConfirmed()
        {
            Status = OrderStatus.Confirmed;
        }
    }
    //pobieranie zamówienia po nr telefonu
}
