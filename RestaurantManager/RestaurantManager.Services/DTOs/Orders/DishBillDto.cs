using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.Orders
{
    public class DishBillDto
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public IEnumerable<IngredientBillDto> Ingredients { get; set; }
    }
}
