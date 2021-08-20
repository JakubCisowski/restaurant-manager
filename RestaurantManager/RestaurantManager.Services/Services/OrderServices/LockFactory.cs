using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public static class LockFactory
    {
        public static object LockObject { get; }
        static LockFactory()
        {
            LockObject = new object();
        }

    }
}
