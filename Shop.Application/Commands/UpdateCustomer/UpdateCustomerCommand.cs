using MediatR;

namespace Shop.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateCustomerCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
