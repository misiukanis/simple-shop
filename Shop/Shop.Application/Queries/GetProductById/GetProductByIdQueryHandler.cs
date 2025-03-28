using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(
        IDbRepository<ProductReadModel> productDbRepository,
        IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productRm = await _productDbRepository.FindByIdAsync(request.Id);
            var productDto = _mapper.Map<ProductDTO>(productRm);

            return productDto;
        }
    }
}
