using System.Collections.Generic;
using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Customers.Commands.CreateCustomer;
using BookStore.Domain.Entities;
using Xunit;

namespace BookStore.Tests.ApplicationTests.Customers.CreateCustomer;

public class CreateCustomerTheoryData : TheoryData<CreateCustomerCommand>
{
    public CreateCustomerTheoryData()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationMappingProfile());
        });
        var mapper = mapperConfiguration.CreateMapper();

        var customers = CustomerFaker.Factory.CreateCustomers(5);
        var commands = mapper.Map<ICollection<Customer>, ICollection<CreateCustomerCommand>>(customers);
        
        foreach (var command in commands)
        {
            Add(command);
        }
    }
}