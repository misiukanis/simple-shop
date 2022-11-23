using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customerRm = await _mongoDbRepository.GetByIdAsync<CustomerReadModel>(request.Id);
            var customerDto = _mapper.Map<CustomerDTO>(customerRm);

            return customerDto;
        }
    }
}
