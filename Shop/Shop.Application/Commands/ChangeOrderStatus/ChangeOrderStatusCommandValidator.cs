using FluentValidation;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.OrderStatus)
                .IsInEnum();
        }
    }
}
