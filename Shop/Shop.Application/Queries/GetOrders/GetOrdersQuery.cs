using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {
    }
}
