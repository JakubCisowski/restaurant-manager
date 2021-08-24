using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.RestaurantCommands.Dishes
{
    public class RemoveIngredientCommand
    {
        public Guid DishId { get; set; }
        public Guid IngredientId { get; set; }
    }
}
