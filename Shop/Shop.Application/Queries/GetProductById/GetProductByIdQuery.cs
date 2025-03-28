using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetProductById
{
    public class GetProductByIdQuery(
        Guid id) : IRequest<ProductDTO>
    {
        public Guid Id { get; } = id;
    }
}
