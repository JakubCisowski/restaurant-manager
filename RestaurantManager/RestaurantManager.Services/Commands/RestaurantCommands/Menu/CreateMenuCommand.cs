using System;

namespace RestaurantManager.Services.Commands.Menu
{
    public class CreateMenuCommand
    {
        public CreateMenuCommand(Guid menuId, Guid restaurantId)
        {
            MenuId = menuId;
            RestaurantId = restaurantId;
        }

        public Guid MenuId { get; }
        public Guid RestaurantId { get; }
    }
}
