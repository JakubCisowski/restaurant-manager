using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.RestaurantQueries.Dishes
{
    public class GetDishQuery
    {
        public GetDishQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
