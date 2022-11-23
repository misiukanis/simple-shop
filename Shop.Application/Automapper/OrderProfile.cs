using AutoMapper;
using Shop.Application.ReadModels;
using Shop.Shared.DTOs;

namespace Shop.Application.Automapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderReadModel, OrderDTO>();
            CreateMap<OrderItemReadModel, OrderItemDTO>();
        }
    }
}
