using AutoMapper;
using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(
            IMongoDbRepository mongoDbRepository,
            IMapper mapper)
        {
            _mongoDbRepository = mongoDbRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productRm = await _mongoDbRepository.GetByIdAsync<ProductReadModel>(request.Id);
            var productDto = _mapper.Map<ProductDTO>(productRm);

            return productDto;
        }
    }
}
