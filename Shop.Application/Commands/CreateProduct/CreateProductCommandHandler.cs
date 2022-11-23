using MediatR;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository;

        public CreateProductCommandHandler(
            IDomainEventDispatcher domainEventDispatcher,
            IEventStoreRepository eventStoreRepository)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Id, request.Name, request.Price);

            await _eventStoreRepository.SaveAsync(product);
            await _domainEventDispatcher.DispatchEventsAsync(product);

            return Unit.Value;
        }
    }
}
