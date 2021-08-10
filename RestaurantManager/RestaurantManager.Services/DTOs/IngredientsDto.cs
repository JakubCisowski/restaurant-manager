using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs
{
    public class IngredientsDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public virtual ICollection<Dish> Dishes { get;  set; } = default!;
    }
}
