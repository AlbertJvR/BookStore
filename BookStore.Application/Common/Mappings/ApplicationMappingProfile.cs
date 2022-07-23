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
        CreateMap<AddressVm, Address>().ReverseMap();
        CreateMap<List<AddressVm>, List<Address>>().ReverseMap();
        CreateMap<Customer, CustomerVm>().ReverseMap();
        CreateMap<OrderVm, Order>().ReverseMap();
        CreateMap<List<OrderVm>, List<Order>>().ReverseMap();
        CreateMap<OrderItemVm, OrderItem>().ReverseMap();
        CreateMap<List<OrderItemVm>, List<OrderItem>>().ReverseMap();
        
        CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
        CreateMap<UpdateCustomerDetailsCommand, UpdateCustomerRequest>().ReverseMap();
    }
}