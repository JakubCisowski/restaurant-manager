using RestaurantManager.Services.DTOs.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.OrderItems
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public decimal DishPrice { get; set; }
        public string DishComment { get; set; }
        public IEnumerable<IngredientBaseDto> DishExtraIngredients { get;  set; }
    }
}
