using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.EventHandlers
{
    public class ProductCreatedEventHandler(
        IDbRepository<ProductReadModel> productDbRepository,
        IMapper mapper) : INotificationHandler<ProductCreatedEvent>
    {
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var productRm = _mapper.Map<ProductReadModel>(notification);
            await _productDbRepository.InsertAsync(productRm);
        }
    }
}
