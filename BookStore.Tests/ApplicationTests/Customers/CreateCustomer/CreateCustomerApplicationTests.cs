using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Models;
using BookStore.Application.Customers.Commands.CreateCustomer;
using BookStore.Tests.Common;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace BookStore.Tests.ApplicationTests.Customers.CreateCustomer;

[Collection(nameof(CustomerCollection))]
public class CreateCustomerApplicationTests
{
    private readonly SharedTestFixture _sharedTestFixture;
    private readonly IMapper _mapper;
    private readonly CreateCustomerCommandValidator _createCustomerValidator;
    private readonly AddressValidator _addressValidator;

    public CreateCustomerApplicationTests(SharedTestFixture sharedTestFixture)
    {
        _sharedTestFixture = sharedTestFixture ?? throw new ArgumentNullException(nameof(sharedTestFixture));
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationMappingProfile());
        });
        _mapper = mockMapper.CreateMapper();
        _createCustomerValidator = new CreateCustomerCommandValidator();
        _addressValidator = new AddressValidator();
    }

    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_FirstNameValid(CreateCustomerCommand command)
    {
        command.FirstName = string.Empty;

        var result = await _createCustomerValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_SurnameValid(CreateCustomerCommand command)
    {
        command.Surname = string.Empty;
        
        var result = await _createCustomerValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Surname).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AtLeastOneAddress(CreateCustomerCommand command)
    {
        command.Addresses = new List<AddressVm>();
        
        var result = await _createCustomerValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Addresses).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressLine1Valid(CreateCustomerCommand command)
    {
        var address = command.Addresses.First();
        address.AddressLine1 = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.AddressLine1).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressLine2Valid(CreateCustomerCommand command)
    {
        var address = command.Addresses.First();
        address.AddressLine2 = "a";
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.AddressLine2).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressSuburbValid(CreateCustomerCommand command)
    {
        var address = command.Addresses.First();
        address.Suburb = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.Suburb).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressCityValid(CreateCustomerCommand command)
    {
        var address = command.Addresses.First();
        address.City = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.City).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressPostalCodeValid(CreateCustomerCommand command)
    {
        var address = command.Addresses.First();
        address.PostalCode = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode).Only();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_SuccessfulCreate(CreateCustomerCommand command)
    {
        // Create the handler
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);

        // Perform the create operation
        var newCustomerId = await generatePdfCommandHandler.Handle(command, CancellationToken.None);
        
        // Query the in memory db for the record that should have been created above. I used first as it should exception if this did not work.
        var createdCustomer = await _sharedTestFixture.BookStoreDbContext.Customers
            .FirstAsync(x => x.Id == newCustomerId, CancellationToken.None);
        
        // Ensure that what was created is what we sent in via the command
        createdCustomer.FirstName.ShouldMatch(command.FirstName);
        createdCustomer.Surname.ShouldMatch(command.Surname);

        foreach (var address in command.Addresses)
        {
            // There is probably a better way to test that addresses were created, but you get the point.
            createdCustomer.Addresses.FirstOrDefault(x => x.AddressLine1 == address.AddressLine1).ShouldNotBeNull();
            createdCustomer.Addresses.FirstOrDefault(x => x.Suburb == address.Suburb).ShouldNotBeNull();
            createdCustomer.Addresses.FirstOrDefault(x => x.City == address.City).ShouldNotBeNull();
            createdCustomer.Addresses.FirstOrDefault(x => x.PostalCode == address.PostalCode).ShouldNotBeNull();
        }
    }
}