using AutoMapper;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Events;

namespace Shop.Application.Automapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductReadModel, ProductDTO>();
            CreateMap<ProductCreatedEvent, ProductReadModel>();
            CreateMap<ProductChangedEvent, ProductReadModel>();
        }
    }
}
