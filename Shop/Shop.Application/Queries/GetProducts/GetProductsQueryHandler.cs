using AutoMapper;
using MediatR;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Queries.GetProducts
{
    public class GetProductsQueryHandler(
        IDbRepository<ProductReadModel> productDbRepository,
        IMapper mapper) : IRequestHandler<GetProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IDbRepository<ProductReadModel> _productDbRepository = productDbRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productsRm = await _productDbRepository.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(productsRm);

            return productsDto;
        }
    }
}
