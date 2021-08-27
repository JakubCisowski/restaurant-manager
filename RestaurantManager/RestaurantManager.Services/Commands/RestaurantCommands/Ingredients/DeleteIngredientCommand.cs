using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.RestaurantCommands.Ingredients
{
    public class DeleteIngredientCommand
    {
        public DeleteIngredientCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
