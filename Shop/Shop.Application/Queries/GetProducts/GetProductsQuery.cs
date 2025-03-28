using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }
}
