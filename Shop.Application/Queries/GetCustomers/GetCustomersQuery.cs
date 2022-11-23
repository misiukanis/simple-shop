using MediatR;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }
}
