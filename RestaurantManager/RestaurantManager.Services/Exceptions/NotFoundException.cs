using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Guid id, string entityName) : base($"Not found '{entityName}' entity for declared id: {id}")
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
