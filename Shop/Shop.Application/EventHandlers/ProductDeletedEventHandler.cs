using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class ProductDeletedEventHandler(
        IDbRepository<ProductReadModel> productDbRepository) : INotificationHandler<ProductDeletedEvent>
    {
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;

        public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _productDbRepository.DeleteAsync(notification.Id);
        }
    }
}
