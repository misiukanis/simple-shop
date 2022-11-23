using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public CustomerCreatedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = _mapper.Map<CustomerReadModel>(notification);
            await _mongoDbRepository.InsertAsync<CustomerReadModel>(customerRm);
        }
    }
}
