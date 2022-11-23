using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customersRm = await _mongoDbRepository.GetAllAsync<CustomerReadModel>();
            var customersDto = _mapper.Map<IEnumerable<CustomerDTO>>(customersRm);

            return customersDto;
        }
    }
}
