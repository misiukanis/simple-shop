using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler(
        IDbRepository<CustomerReadModel> customerDbRepository,
        IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly IDbRepository<CustomerReadModel> _customerDbRepository = customerDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customerRm = await _customerDbRepository.FindByIdAsync(request.Id);
            var customerDto = _mapper.Map<CustomerDTO>(customerRm);

            return customerDto;
        }
    }
}
