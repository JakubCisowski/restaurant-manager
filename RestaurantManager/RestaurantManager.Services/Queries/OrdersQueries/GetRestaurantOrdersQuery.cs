using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.OrdersQueries
{
    public class GetRestaurantOrdersQuery
    {
        public Guid RestaurantId { get; }
        public GetRestaurantOrdersQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
