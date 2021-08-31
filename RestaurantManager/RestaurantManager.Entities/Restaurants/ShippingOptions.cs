using System;

namespace RestaurantManager.Entities.Restaurants
{
    public class ShippingOptions : Entity
    {
        public ShippingOptions(double maxShippingDistanceRadius)
        {
            MaxShippingDistanceRadius = maxShippingDistanceRadius;
        }

        public double MaxShippingDistanceRadius { get; private set; }

        public Guid RestaurantId { get; private set; }
        public virtual Restaurant Restaurant { get; private set; }
    }
}