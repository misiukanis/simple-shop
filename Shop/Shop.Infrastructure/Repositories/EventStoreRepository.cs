using EventStore.Client;
using Shop.Domain.Core;
using Shop.Domain.Interfaces.Repositories;
using System.Text;
using System.Text.Json;

namespace Shop.Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreClient _eventStoreClient;


        public EventStoreRepository(EventStoreClient eventStoreClient)
        {
            _eventStoreClient = eventStoreClient;
        }


        public async Task<T> LoadAsync<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentException(nameof(aggregateId));

            var streamName = GetStreamName<T>(aggregateId);            

            var readStreamResult = _eventStoreClient.ReadStreamAsync(
                    Direction.Forwards,
                    streamName,
                    StreamPosition.Start);

            if (await readStreamResult.ReadState == ReadState.StreamNotFound)
                return null;

            var events = new List<IDomainEvent>();
            await foreach (var @event in readStreamResult)
            {
                var json = Encoding.UTF8.GetString(@event.Event.Data.ToArray());
                var type = Type.GetType(Encoding.UTF8.GetString(@event.Event.Metadata.ToArray()));
                var @object = JsonSerializer.Deserialize(json, type);
                var domainEvent = (IDomainEvent)@object;

                events.Add(domainEvent);
            }

            var aggregate = new T();
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public async Task SaveAsync<T>(T aggregate) where T : AggregateRoot, new()
        {
            var events = aggregate.GetDomainEvents();
            if (!events.Any())
                return;

            var streamName = GetStreamName<T>(aggregate.Id);

            var eventsToSave = new List<EventData>();
            foreach (var @event in events)
            {
                var eventData = new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().Name,
                    JsonSerializer.SerializeToUtf8Bytes((object)@event),
                    Encoding.UTF8.GetBytes(@event.GetType().AssemblyQualifiedName));

                eventsToSave.Add(eventData);
            }

            await _eventStoreClient.AppendToStreamAsync(streamName, StreamState.Any, eventsToSave);
        }

        private string GetStreamName<T>(Guid aggregateId)
        {
            var streamName = $"{typeof(T).Name}-{aggregateId}";
            return streamName;
        }
    }
}
