using MediatR;

namespace Shop.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand(
        Guid id, 
        string name) : IRequest
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
    }
}
