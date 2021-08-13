using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Entities.Orders
{
    public class OrderItem : Entity
    {
        public string DishName { get; private set; }
        public decimal DishPrice { get; private set; }
        public string DishComment { get; private set; }

        public ICollection<DishExtraIngredient> DishExtraIngredients { get; private set; } = new List<DishExtraIngredient>();

        public Guid OrderId { get; private set; }

        public virtual Order Order { get; private set; }

    }
}
