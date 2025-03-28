using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(
        IDbRepository<OrderReadModel> orderDbRepository,
        IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IDbRepository<OrderReadModel> _orderDbRepository = orderDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderRm = await _orderDbRepository.FindByIdAsync(request.Id);
            var orderDto = _mapper.Map<OrderDTO>(orderRm);

            return orderDto;
        }
    }
}
