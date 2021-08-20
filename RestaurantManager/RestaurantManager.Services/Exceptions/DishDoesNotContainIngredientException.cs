using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    public class DishDoesNotContainIngredientException : Exception
    {
        public DishDoesNotContainIngredientException(Guid dishId) : base($"One or more Ingredient isn't available for the Dish with id: {dishId}")
        {
        }

        public DishDoesNotContainIngredientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DishDoesNotContainIngredientException(string message) : base(message)
        {
        }
    }
}
