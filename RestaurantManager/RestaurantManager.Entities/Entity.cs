using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Entities
{
    public class Entity
    {
        public Guid Id { get; protected set; }
    }
}
