using MediatR;

namespace Shop.Application.Commands.DeleteProduct
{
    public class DeleteProductCommand(
        Guid id) : IRequest
    {
        public Guid Id { get; } = id;
    }
}
