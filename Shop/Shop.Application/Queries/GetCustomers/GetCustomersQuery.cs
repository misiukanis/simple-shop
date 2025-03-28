using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }
}
