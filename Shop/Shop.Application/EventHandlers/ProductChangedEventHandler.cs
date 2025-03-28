using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class ProductChangedEventHandler(
        IDbRepository<ProductReadModel> productDbRepository,
        IMapper mapper) : INotificationHandler<ProductChangedEvent>
    {
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(ProductChangedEvent notification, CancellationToken cancellationToken)
        {
            var productRm = _mapper.Map<ProductReadModel>(notification);
            await _productDbRepository.UpdateAsync(notification.Id, productRm);
        }
    }
}
