using System;

namespace RestaurantManager.Entities.Restaurants
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {
        }

        public Restaurant(Guid id, string name, string phone, string address)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;

        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public virtual Menu Menu { get; private set; } = default!;
    }
}
