using MediatR;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }
}
