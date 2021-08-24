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
    }
}
