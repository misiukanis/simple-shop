using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class OrderStatusChangedEventHandler(
        IDbRepository<OrderReadModel> orderDbRepository) : INotificationHandler<OrderStatusChangedEvent>
    {
        private readonly IDbRepository<OrderReadModel> _orderDbRepository = orderDbRepository;

        public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _orderDbRepository.GetByIdAsync(notification.Id);
            orderRm.OrderStatus = notification.OrderStatus;
            
            await _orderDbRepository.UpdateAsync(notification.Id, orderRm);
        }
    }
}
