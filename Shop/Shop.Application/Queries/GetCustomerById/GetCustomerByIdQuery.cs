using MediatR;
using Shop.Application.DTOs;

namespace Shop.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery(
        Guid id) : IRequest<CustomerDTO>
    {
        public Guid Id { get; } = id;
    }
}
