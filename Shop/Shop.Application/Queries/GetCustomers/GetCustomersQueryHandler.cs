using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQueryHandler(
        IDbRepository<CustomerReadModel> customerDbRepository,
        IMapper mapper) : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private readonly IDbRepository<CustomerReadModel> _customerDbRepository = customerDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customersRm = await _customerDbRepository.GetAllAsync();
            var customersDto = _mapper.Map<IEnumerable<CustomerDTO>>(customersRm);

            return customersDto;
        }
    }
}
