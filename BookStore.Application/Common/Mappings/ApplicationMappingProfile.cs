using AutoMapper;
using BookStore.Application.Common.Models;
using BookStore.Application.Customers.Commands.CreateCustomer;
using BookStore.Application.Customers.Commands.UpdateCustomer;
using BookStore.Domain.Entities;

namespace BookStore.Application.Common.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<AddressVm, Address>()
            .ForMember(d => d.Customers, c => c.Ignore())
            .ReverseMap();
        CreateMap<Customer, CustomerVm>()
            .ForMember(d => d.Orders, c => c.Ignore())
            .ReverseMap();
        CreateMap<OrderVm, Order>()
            .ReverseMap();
        CreateMap<OrderItemVm, OrderItem>()
            .ReverseMap();
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(d => d.Id, c => c.Ignore())
            .ForMember(d => d.Orders, c => c.Ignore())
            .ReverseMap();
        CreateMap<UpdateCustomerDetailsCommand, Customer>()
            .ForMember(d => d.Id, c => c.Ignore())
            .ReverseMap();
        CreateMap<UpdateCustomerDetailsCommand, UpdateCustomerRequest>().ReverseMap();
    }
}