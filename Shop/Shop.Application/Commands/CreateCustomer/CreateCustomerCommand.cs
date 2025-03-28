using MediatR;

namespace Shop.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand(
        Guid id, 
        string name) : IRequest
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
    }
}
