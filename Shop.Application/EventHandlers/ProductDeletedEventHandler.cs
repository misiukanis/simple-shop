using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public ProductDeletedEventHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _mongoDbRepository.DeleteAsync<ProductReadModel>(notification.Id);
        }
    }
}
