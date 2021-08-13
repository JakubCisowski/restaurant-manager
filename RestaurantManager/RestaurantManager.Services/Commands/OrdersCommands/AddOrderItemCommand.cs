using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.Orders
{
    public class AddOrderItemCommand
    {
        public Guid OrderId { get; set; }
        public Guid DishId { get; set; }
        public ICollection<Guid> ExtraIngredientIds { get; set; }

    }
}
