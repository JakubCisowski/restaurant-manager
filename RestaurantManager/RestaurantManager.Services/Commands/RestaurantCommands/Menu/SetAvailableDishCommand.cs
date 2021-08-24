using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.RestaurantCommands.Menu
{
    public class SetAvailableDishCommand
    {
        public Guid MenuId { get; set; }
        public Guid DishId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
