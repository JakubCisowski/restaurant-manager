using System;

namespace RestaurantManager.Entities.Restaurants
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {
        }

        public Restaurant(Guid id, string name, string phone, RestaurantAddress address, double maxShippingDistanceRadius)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
            ShippingOptions = new ShippingOptions(maxShippingDistanceRadius);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        public void SetAddress(RestaurantAddress address)
        {
            Address = address;
        }
        public void AddMenu(Menu menu)
        {
            Menu = menu;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public virtual RestaurantAddress Address { get; private set; }
        public virtual ShippingOptions ShippingOptions { get; private set; } = default!;
        public virtual Menu Menu { get; private set; } = default!;

    }
}
