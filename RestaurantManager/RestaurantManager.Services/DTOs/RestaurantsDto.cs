﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.DTOs
{
    public class RestaurantsDto
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Phone { get;  set; }
        public string Address { get;  set; }
    }
}
