using MediatR;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        public Guid Id { get; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
