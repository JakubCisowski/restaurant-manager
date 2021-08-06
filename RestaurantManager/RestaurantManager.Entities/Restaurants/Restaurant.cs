using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities.Restaurants
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {

        }

        public Restaurant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }
}
