using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Consts.Enums
{
    public enum OrderStatus
    {
        New,
        Confirmed,
        Paid,
        Processing,
        InDelivery,
        Finished,
        Canceled
    }
}
