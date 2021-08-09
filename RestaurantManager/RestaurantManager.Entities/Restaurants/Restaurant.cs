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

        public void ChangeName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }

    }
}
