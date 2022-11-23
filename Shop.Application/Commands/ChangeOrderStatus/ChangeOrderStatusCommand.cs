using MediatR;
using Shop.Shared.Enums;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand : IRequest
    {
        public Guid Id { get; }
        public OrderStatus OrderStatus { get; }

        public ChangeOrderStatusCommand(Guid id, OrderStatus orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
        }
    }
}
