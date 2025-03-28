using MediatR;

namespace Shop.Application.Commands.CreateProduct
{
    public class CreateProductCommand(
        Guid id, 
        string name, 
        decimal price) : IRequest
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public decimal Price { get; } = price;
    }
}
