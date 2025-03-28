using AutoMapper;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;

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
