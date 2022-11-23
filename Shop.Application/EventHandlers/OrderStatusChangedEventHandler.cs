using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.Enums;

namespace Shop.Application.EventHandlers
{
    public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public OrderStatusChangedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _mongoDbRepository.GetByIdAsync<OrderReadModel>(notification.Id);
            orderRm.OrderStatus = (OrderStatus)notification.OrderStatus;
            
            await _mongoDbRepository.UpdateAsync<OrderReadModel>(notification.Id, orderRm);
        }
    }
}
