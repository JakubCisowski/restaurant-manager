using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.Menu
{
    public class CreateMenuCommand
    {
        public Guid RestaurantId { get; set; }
    }
}
