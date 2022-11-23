using MediatR;

namespace Shop.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public CreateProductCommand(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
