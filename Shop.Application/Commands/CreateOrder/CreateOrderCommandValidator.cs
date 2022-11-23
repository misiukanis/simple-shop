using FluentValidation;

namespace Shop.Application.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.OrderItems)
                .NotEmpty();

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
