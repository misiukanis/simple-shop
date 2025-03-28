using MediatR;
using Shop.Domain.Aggregates.CustomerAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Id, request.Name);

            await _eventStoreRepository.SaveAsync(customer);
            await _domainEventDispatcher.DispatchEventsAsync(customer);
        }
    }
}
