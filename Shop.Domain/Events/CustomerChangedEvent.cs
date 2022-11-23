using MediatR;

namespace Shop.Domain.Events
{
    public class CustomerChangedEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }

        public CustomerChangedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
