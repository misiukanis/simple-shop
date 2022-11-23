using MediatR;

namespace Shop.Domain.Core
{
    public abstract class AggregateRoot
    {
        private readonly IList<INotification> _domainEvents = new List<INotification>();
        public Guid Id { get; protected set; }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return _domainEvents.AsEnumerable();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void LoadFromHistory(IEnumerable<INotification> events)
        {
            foreach (var @event in events)
            {
                When(@event);
            }
        }

        protected void ApplyChange(INotification @event)
        {
            When(@event);
            _domainEvents.Add(@event);
        }

        protected abstract void When(INotification @event);
    }
}
