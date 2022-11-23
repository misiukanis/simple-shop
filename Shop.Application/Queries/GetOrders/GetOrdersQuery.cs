using MediatR;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {
    }
}
