using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Aggregates.CustomerAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _eventStoreRepository.LoadAsync<Customer>(request.Id);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), nameof(Customer.Id), request.Id);
            }

            customer.Change(request.Name);

            await _eventStoreRepository.SaveAsync(customer);
            await _domainEventDispatcher.DispatchEventsAsync(customer);
        }
    }
}
