using System;

namespace RestaurantManager.Entities.Restaurants
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {
        }

        public Restaurant(string name, string phone, string address)
        {
            Id = Guid.NewGuid();
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

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }

    }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }
}
