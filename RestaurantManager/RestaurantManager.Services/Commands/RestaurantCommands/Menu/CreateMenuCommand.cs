using System;

namespace RestaurantManager.Services.Commands.Menu
{
    public class CreateMenuCommand
    {
        public CreateMenuCommand(Guid menuId, Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}
