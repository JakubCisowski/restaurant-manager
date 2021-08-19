using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.ErrorResponses
{
    public class FilterErrorResponse
    {

        public FilterErrorResponse(string filter, string message)
        {
            Filter = filter;
            ExceptionMessage = message;
        }

        public string Filter { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
