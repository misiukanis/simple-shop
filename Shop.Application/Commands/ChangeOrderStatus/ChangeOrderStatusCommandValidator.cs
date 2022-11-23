using FluentValidation;
using Shop.Shared.Enums;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.OrderStatus)
                .Must(EnumIsDefined);
        }

        private bool EnumIsDefined(OrderStatus orderStatus)
        {
            bool enumIsDefined = Enum.IsDefined(typeof(Domain.Aggregates.OrderAggregate.OrderStatus), (int)orderStatus);
            return enumIsDefined;
        }
    }
}
