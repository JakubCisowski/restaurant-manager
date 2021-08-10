using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Inputs.Restaurants
{
    public class IngredientInput
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
