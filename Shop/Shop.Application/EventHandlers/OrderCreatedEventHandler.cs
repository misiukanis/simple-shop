using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class OrderCreatedEventHandler(
        IDbRepository<OrderReadModel> orderDbRepository,
        IDbRepository<CustomerReadModel> customerDbRepository) : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IDbRepository<OrderReadModel> _orderDbRepository = orderDbRepository;
        private readonly IDbRepository<CustomerReadModel> _customerDbRepository = customerDbRepository;

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = await _customerDbRepository.GetByIdAsync(notification.CustomerId);

            var orderRm = new OrderReadModel();
            orderRm.Id = notification.Id;
            orderRm.CustomerId = notification.CustomerId;
            orderRm.CustomerName = customerRm.Name;
            orderRm.City = notification.Address.City;
            orderRm.Street = notification.Address.Street;
            orderRm.OrderStatus = notification.OrderStatus;
            orderRm.CreationDate = notification.CreationDate;
            orderRm.OrderItems = new List<OrderItemReadModel>();

            await _orderDbRepository.InsertAsync(orderRm);
        }
    }
}
