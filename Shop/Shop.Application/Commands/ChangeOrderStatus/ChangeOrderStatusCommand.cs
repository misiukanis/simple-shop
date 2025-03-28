using MediatR;
using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand(
        Guid id, 
        OrderStatus orderStatus) : IRequest
    {
        public Guid Id { get; } = id;
        public OrderStatus OrderStatus { get; } = orderStatus;
    }
}
