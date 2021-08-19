using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs.Orders
{
    public class DinnerBillDto
    {
        public int OrderNo { get; set; }
        public string Phone { get; set; }
        public IEnumerable<DishBillDto> Dishes { get; set; }
        public float TotalPrice { get; set; }
    }
}
