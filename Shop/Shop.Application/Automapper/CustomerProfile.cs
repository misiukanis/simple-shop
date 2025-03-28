using AutoMapper;
using Shop.Application.DTOs;
using Shop.Application.ReadModels;
using Shop.Domain.Events;

namespace Shop.Application.Automapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerReadModel, CustomerDTO>();
            CreateMap<CustomerCreatedEvent, CustomerReadModel>();
            CreateMap<CustomerChangedEvent, CustomerReadModel>();
        }
    }
}
