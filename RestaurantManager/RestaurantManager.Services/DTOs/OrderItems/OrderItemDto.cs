using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.OrderItems
{
    public class OrderItemDto
    {
        public string DishName { get;  set; }
        public decimal DishPrice { get;  set; }
        public string DishComment { get;  set; }
        public Guid Id { get;  set; }
        // public ICollection<DishExtraIngredient> DishExtraIngredients { get;  set; }
        //public Guid OrderId { get;  set; }
        //public virtual Order Order { get;  set; }
    }
}
