using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Integration.Model
{
    public class CoordinatesResponse
    {
        public CoordinatesResponse(List<double> coordinates)
        {
            Longitude = coordinates[0];
            Latitude = coordinates[1];
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
