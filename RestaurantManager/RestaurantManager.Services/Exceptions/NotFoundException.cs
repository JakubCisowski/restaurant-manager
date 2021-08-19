using System;

namespace RestaurantManager.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public Guid Id { get; }
        public string Filter { get; }

        public NotFoundException(Guid id, string entityName) : base($"Not found '{entityName}' entity for declared id: {id}")
        {
            Id = id;
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string filter, string entityName) : this($"Not found '{entityName}' entity for declared request data: {filter}")
        {
            Filter = filter;
        }

    }
}
