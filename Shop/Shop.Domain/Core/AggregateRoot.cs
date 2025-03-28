namespace Shop.Domain.Core
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                When(@event);
            }
        }

        protected void ApplyChange(IDomainEvent @event)
        {
            When(@event);
            _domainEvents.Add(@event);
        }

        protected abstract void When(IDomainEvent @event);
    }
}
