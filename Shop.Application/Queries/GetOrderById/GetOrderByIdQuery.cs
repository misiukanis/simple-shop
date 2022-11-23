using MediatR;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public Guid Id { get; }

        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
