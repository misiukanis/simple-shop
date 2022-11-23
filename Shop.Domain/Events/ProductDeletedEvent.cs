using MediatR;

namespace Shop.Domain.Events
{
    public class ProductDeletedEvent : INotification
    {
        public Guid Id { get; }

        public ProductDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
