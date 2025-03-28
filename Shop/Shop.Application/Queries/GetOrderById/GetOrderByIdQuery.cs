using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery(
        Guid id) : IRequest<OrderDTO>
    {
        public Guid Id { get; } = id;
    }
}
