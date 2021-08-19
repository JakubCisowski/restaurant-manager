using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Inputs.Orders
{
    public class OrderItemInput
    {
        public int OrderNo { get; set; }
        public Guid DishId { get; set; }
        public string DishComment { get; set; }
        public ICollection<Guid> ExtraIngredientIds { get; set; }
    }
}
