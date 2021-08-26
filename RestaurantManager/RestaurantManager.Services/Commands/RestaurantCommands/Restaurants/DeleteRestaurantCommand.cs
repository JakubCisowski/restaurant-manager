using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.RestaurantCommands.Restaurants
{
    public class DeleteRestaurantCommand
    {
        public DeleteRestaurantCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
