using MediatR;

namespace Shop.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public CreateCustomerCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
