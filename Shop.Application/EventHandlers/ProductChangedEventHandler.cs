using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class ProductChangedEventHandler : INotificationHandler<ProductChangedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public ProductChangedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(ProductChangedEvent notification, CancellationToken cancellationToken)
        {
            var productRm = _mapper.Map<ProductReadModel>(notification);
            await _mongoDbRepository.UpdateAsync<ProductReadModel>(notification.Id, productRm);
        }
    }
}
