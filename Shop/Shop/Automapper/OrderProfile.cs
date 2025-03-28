using AutoMapper;
using Shop.Application.Commands.CreateOrder;
using Shop.Models.Requests;

namespace Shop.Automapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>()
            .ConstructUsing(src => new CreateOrderCommand(
                Guid.NewGuid(),
                src.CustomerId.Value,
                src.City,
                src.Street,
                src.OrderItems.Select(item => new CreateOrderItemCommand(item.ProductId, item.Quantity)).ToList()
            ));

            CreateMap<CreateOrderItemRequest, CreateOrderItemCommand>()
                .ConstructUsing(src => new CreateOrderItemCommand(src.ProductId, src.Quantity));
        }
    }
}
