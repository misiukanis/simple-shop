using MediatR;

namespace Shop.Domain.Events
{
    public class CustomerCreatedEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }

        public CustomerCreatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
