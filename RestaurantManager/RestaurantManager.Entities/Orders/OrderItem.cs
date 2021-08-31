using RestaurantManager.Entities.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManager.Entities.Orders
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {

        }

        public OrderItem(Guid id, Guid orderId, int orderNo, Dish dish, string dishComment, List<DishExtraIngredient> extraIngredients)
        {
            Id = id;
            OrderNo = orderNo;
            DishComment = dishComment;
            DishName = dish.Name;
            DishExtraIngredients = extraIngredients;
            DishPrice = dish.BasePrice;
            OrderId = orderId;
        }

        public string DishName { get; private set; }
        public decimal DishPrice { get; private set; }
        public string DishComment { get; private set; }

        public virtual ICollection<DishExtraIngredient> DishExtraIngredients { get; private set; } = new List<DishExtraIngredient>();

        public Guid OrderId { get; private set; }

        public int OrderNo { get; private set; }

        public virtual Order Order { get; private set; }

        public void SetOrder(Order order)
        {
            Order = order;
        }
    }
}
