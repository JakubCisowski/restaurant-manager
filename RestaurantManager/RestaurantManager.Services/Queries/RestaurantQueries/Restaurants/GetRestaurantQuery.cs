using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.Restaurants
{
    public class GetRestaurantQuery
    {
        public GetRestaurantQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
