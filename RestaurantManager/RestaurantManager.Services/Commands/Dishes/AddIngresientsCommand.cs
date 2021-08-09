using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class AddIngresientsCommand
    {
        public Guid DishId { get; set; }
        public  IEnumerable<Guid> IngredientIds { get; set; }

    }

}
