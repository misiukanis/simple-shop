using MediatR;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateProductCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Id, request.Name, request.Price);

            await _eventStoreRepository.SaveAsync(product);
            await _domainEventDispatcher.DispatchEventsAsync(product);
        }
    }
}
