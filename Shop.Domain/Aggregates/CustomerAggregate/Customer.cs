using MediatR;
using Shop.Domain.Core;
using Shop.Domain.Events;

namespace Shop.Domain.Aggregates.CustomerAggregate
{
    public class Customer : AggregateRoot
    {
        public string Name { get; private set; }


        public Customer() { } 

        public Customer(Guid id, string name)
        {
            ApplyChange(new CustomerCreatedEvent(id, name));
        }


        public void Change(string name)
        {
            ApplyChange(new CustomerChangedEvent(Id, name));
        }


        protected override void When(INotification @event)
        {
            switch (@event)
            {
                case CustomerCreatedEvent e:
                    Id = e.Id;
                    Name = e.Name;
                    break;
                case CustomerChangedEvent e:
                    Name = e.Name;
                    break;
            }
        }
    }
}
