using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;

namespace RestaurantManager.Entities.Orders
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {

        }

        public OrderItem(Guid id, Guid orderId, Dish dish, string dishComment, ICollection<DishExtraIngredient> extraIngredients)
        {
            Id = id;
            OrderId = orderId;
            DishComment = dishComment;
            DishName = dish.Name;
            DishExtraIngredients = extraIngredients;
            // DishPrice = dish.BasePrice; // tutaj nie wiem, co oznacza ta cena, czy to jest łączna Disha z ExtraIngredientami?
        }

        public string DishName { get; private set; }
        public decimal DishPrice { get; private set; }
        public string DishComment { get; private set; }

        public ICollection<DishExtraIngredient> DishExtraIngredients { get; private set; } = new List<DishExtraIngredient>();

        public Guid OrderId { get; private set; }

        public virtual Order Order { get; private set; }

        public void SetOrder(Order order)
        {
            Order = order;
        }
    }
}
