using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<UpdateProductCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _eventStoreRepository.LoadAsync<Product>(request.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), nameof(Product.Id), request.Id);
            }

            product.Change(request.Name, request.Price);

            await _eventStoreRepository.SaveAsync(product);
            await _domainEventDispatcher.DispatchEventsAsync(product);
        }
    }
}
