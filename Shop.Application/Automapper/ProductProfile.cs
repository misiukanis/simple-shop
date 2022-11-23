using AutoMapper;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Shared.DTOs;

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
