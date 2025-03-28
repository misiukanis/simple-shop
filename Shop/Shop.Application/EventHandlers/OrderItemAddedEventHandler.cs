using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class OrderItemAddedEventHandler(
        IDbRepository<OrderReadModel> orderDbRepository,
        IDbRepository<ProductReadModel> productDbRepository) : INotificationHandler<OrderItemAddedEvent>
    {
        private readonly IDbRepository<OrderReadModel> _orderDbRepository = orderDbRepository;
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;

        public async Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _orderDbRepository.GetByIdAsync(notification.OrderId);

            var orderItemRm = orderRm.OrderItems.SingleOrDefault(x => x.ProductId == notification.ProductId);
            if (orderItemRm != null)
            {
                orderItemRm.Quantity += notification.Quantity;
            }
            else
            {
                var productRm = await _productDbRepository.GetByIdAsync(notification.ProductId);

                orderItemRm = new OrderItemReadModel();
                orderItemRm.ProductId = notification.ProductId;
                orderItemRm.ProductName = productRm.Name;
                orderItemRm.Quantity = notification.Quantity;
                orderItemRm.Price = notification.Price;
                orderRm.OrderItems.Add(orderItemRm);
            }

            orderRm.TotalQuantity = orderRm.OrderItems.Sum(x => x.Quantity);
            orderRm.TotalPrice = orderRm.OrderItems.Sum(x => x.Quantity * x.Price);

            await _orderDbRepository.UpdateAsync(notification.OrderId, orderRm);
        }
    }
}
