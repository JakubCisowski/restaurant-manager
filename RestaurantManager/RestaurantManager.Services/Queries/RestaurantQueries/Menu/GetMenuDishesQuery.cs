using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Queries.RestaurantQueries.Menu
{
    public class GetMenuDishesQuery
    {
        public GetMenuDishesQuery(Guid menuId, bool displayNonAvailableDishes)
        {
            MenuId = menuId;
            DisplayNonAvailableDishes = displayNonAvailableDishes;
        }

        public Guid MenuId { get; }
        public bool DisplayNonAvailableDishes { get; }
    }
}
