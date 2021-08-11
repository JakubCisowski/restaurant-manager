﻿using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Orders
{
    public class OrderItem : Entity
    {
        public string DishName { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DishPrice { get; private set; }
        public string DishComment { get; private set; }

        public ICollection<DishExtraIngredients> DishExtraIngredients { get; private set; } = new List<DishExtraIngredients>();

        public Guid OrderId { get; private set; }

        public virtual Order Order { get; private set; }

    }
}
