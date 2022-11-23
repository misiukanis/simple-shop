using EventStore.Client;
using MediatR;
using Shop.Domain.Core;
using Shop.Domain.Repositories.Interfaces;
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
            var aggregate = new T();

            var readStreamResult = _eventStoreClient.ReadStreamAsync(
                    Direction.Forwards,
                    streamName,
                    StreamPosition.Start);

            if (await readStreamResult.ReadState == ReadState.StreamNotFound)
                return null;

            var events = new List<INotification>();
            await foreach (var @event in readStreamResult)
            {
                var json = Encoding.UTF8.GetString(@event.Event.Data.ToArray());
                var type = Type.GetType(Encoding.UTF8.GetString(@event.Event.Metadata.ToArray()));
                var @object = JsonSerializer.Deserialize(json, type);
                var notification = (INotification)@object;

                events.Add(notification);
            }

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
                    eventId: Uuid.NewUuid(),
                    type: @event.GetType().Name,
                    data: JsonSerializer.SerializeToUtf8Bytes((object)@event),
                    metadata: Encoding.UTF8.GetBytes(@event.GetType().AssemblyQualifiedName));

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
