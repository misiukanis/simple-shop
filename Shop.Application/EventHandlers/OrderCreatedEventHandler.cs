using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.Enums;

namespace Shop.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public OrderCreatedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = await _mongoDbRepository.GetByIdAsync<CustomerReadModel>(notification.CustomerId);

            var orderRm = new OrderReadModel();
            orderRm.Id = notification.Id;
            orderRm.CustomerId = notification.CustomerId;
            orderRm.CustomerName = customerRm.Name;
            orderRm.City = notification.Address.City;
            orderRm.Street = notification.Address.Street;
            orderRm.OrderStatus = (OrderStatus)notification.OrderStatus;
            orderRm.CreationDate = notification.CreationDate;
            orderRm.OrderItems = new List<OrderItemReadModel>();

            await _mongoDbRepository.InsertAsync<OrderReadModel>(orderRm);
        }
    }
}
