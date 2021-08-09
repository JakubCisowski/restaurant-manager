using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.Dishes
{
    public class CreateDishCommand
    {
        public string Name { get; set; }

        public decimal BasePrice { get; set; }
        public string Description { get; set; }

        public Guid MenuId { get; set; }
    }
}
