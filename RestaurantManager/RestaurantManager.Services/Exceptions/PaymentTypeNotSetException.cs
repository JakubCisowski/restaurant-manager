using RestaurantManager.Consts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    class PaymentTypeNotSetException : Exception
    {
        public PaymentTypeNotSetException() : base()
        {
        }

        public PaymentTypeNotSetException(string message) : base(message)
        {
        }

        public PaymentTypeNotSetException(int orderNo, PaymentType paymentType)
            : this($"(Incorrect order payment type: {paymentType} for order orderNo: {orderNo}.")
        {
            OrderNo = orderNo;
            PaymentType = paymentType;
        }

        public int OrderNo { get; }
        public PaymentType PaymentType { get; }
    }
}
