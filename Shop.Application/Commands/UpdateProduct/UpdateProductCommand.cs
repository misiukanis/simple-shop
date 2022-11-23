using MediatR;

namespace Shop.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public UpdateProductCommand(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
