using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.RestaurantCommands.Dishes
{
    public class DeleteDishCommand
    {
        public DeleteDishCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
