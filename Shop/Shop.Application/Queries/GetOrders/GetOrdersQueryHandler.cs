using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetOrders
{
    public class GetOrdersQueryHandler(
        IDbRepository<OrderReadModel> orderDbRepository,
        IMapper mapper) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly IDbRepository<OrderReadModel> _orderDbRepository = orderDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersRm = await _orderDbRepository.GetAllAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDTO>>(ordersRm);

            return ordersDto;
        }
    }
}
