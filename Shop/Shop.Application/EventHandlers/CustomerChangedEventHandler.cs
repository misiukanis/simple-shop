using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class CustomerChangedEventHandler(
        IDbRepository<CustomerReadModel> customerDbRepository,
        IMapper mapper) : INotificationHandler<CustomerChangedEvent>
    {
        private readonly IDbRepository<CustomerReadModel> _customerDbRepository = customerDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(CustomerChangedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = _mapper.Map<CustomerReadModel>(notification);
            await _customerDbRepository.UpdateAsync(notification.Id, customerRm);
        }
    }
}
