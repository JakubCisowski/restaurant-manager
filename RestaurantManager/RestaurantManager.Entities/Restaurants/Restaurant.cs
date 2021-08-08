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

        public string Name { get;  set; }
        public string Phone { get;  set; }
        public string Address { get;  set; }
    }
}
