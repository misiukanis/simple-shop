using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class OrderItemAddedEventHandler : INotificationHandler<OrderItemAddedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public OrderItemAddedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _mongoDbRepository.GetByIdAsync<OrderReadModel>(notification.OrderId);

            var orderItemRm = orderRm.OrderItems.SingleOrDefault(x => x.ProductId == notification.ProductId);
            if (orderItemRm != null)
            {
                orderItemRm.Quantity += notification.Quantity;
            }
            else
            {
                var productRm = await _mongoDbRepository.GetByIdAsync<ProductReadModel>(notification.ProductId);

                orderItemRm = new OrderItemReadModel();
                orderItemRm.ProductId = notification.ProductId;
                orderItemRm.ProductName = productRm.Name;
                orderItemRm.Quantity = notification.Quantity;
                orderItemRm.Price = notification.Price;
                orderRm.OrderItems.Add(orderItemRm);
            }

            await _mongoDbRepository.UpdateAsync<OrderReadModel>(notification.OrderId, orderRm);
        }
    }
}
