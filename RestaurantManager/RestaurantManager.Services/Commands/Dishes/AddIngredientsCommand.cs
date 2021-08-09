using System;
using System.Collections.Generic;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class AddIngredientsCommand
    {
        public Guid DishId { get; set; }
        public IEnumerable<Guid> IngredientIds { get; set; }

    }

}
