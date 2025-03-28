using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class CustomerCreatedEventHandler(
        IDbRepository<CustomerReadModel> customerDbRepository,
        IMapper mapper) : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IDbRepository<CustomerReadModel> _customerDbRepository = customerDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = _mapper.Map<CustomerReadModel>(notification);
            await _customerDbRepository.InsertAsync(customerRm);
        }
    }
}
