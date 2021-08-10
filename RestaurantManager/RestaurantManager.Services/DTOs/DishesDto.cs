using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs
{
    public class DishesDto
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public decimal BasePrice { get;  set; }
        public string Description { get;  set; }
        public virtual ICollection<Ingredient> Ingredients { get;  set; } = default!;

        public Guid? MenuId { get;  set; }
    }
}
