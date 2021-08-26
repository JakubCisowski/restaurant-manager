using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Commands.OrdersCommands
{
    public class DeleteOrderItemCommand
    {
        public DeleteOrderItemCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
