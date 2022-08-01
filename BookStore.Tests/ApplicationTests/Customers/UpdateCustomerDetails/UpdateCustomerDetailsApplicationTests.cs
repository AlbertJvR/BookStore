using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Models;
using BookStore.Application.Customers.Commands.UpdateCustomer;
using BookStore.Tests.Common;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace BookStore.Tests.ApplicationTests.Customers.UpdateCustomerDetails;

[Collection(nameof(CustomerCollection))]
public class UpdateCustomerApplicationTests
{
    private readonly SharedTestFixture _sharedTestFixture;
    private readonly IMapper _mapper;
    private readonly UpdateCustomerDetailsCommandValidator _updateCustomerDetailsCommandValidator;
    private readonly AddressValidator _addressValidator;

    public UpdateCustomerApplicationTests(SharedTestFixture sharedTestFixture)
    {
        _sharedTestFixture = sharedTestFixture ?? throw new ArgumentNullException(nameof(sharedTestFixture));
        _mapper = Mock.Of<IMapper>();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationMappingProfile());
        });
        _mapper = mockMapper.CreateMapper();
        _updateCustomerDetailsCommandValidator = new UpdateCustomerDetailsCommandValidator();
        _addressValidator = new AddressValidator();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_CustomerIdToUpdateValid(UpdateCustomerDetailsCommand command)
    {
        command.Id = default;

        var result = await _updateCustomerDetailsCommandValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_FirstNameValid(UpdateCustomerDetailsCommand command)
    {
        command.FirstName = string.Empty;

        var result = await _updateCustomerDetailsCommandValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_SurnameValid(UpdateCustomerDetailsCommand command)
    {
        command.Surname = string.Empty;
        
        var result = await _updateCustomerDetailsCommandValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Surname).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AtLeastOneAddress(UpdateCustomerDetailsCommand command)
    {
        command.Addresses = new List<AddressVm>();
        
        var result = await _updateCustomerDetailsCommandValidator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Addresses).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AddressLine1Valid(UpdateCustomerDetailsCommand command)
    {
        var address = command.Addresses.First();
        address.AddressLine1 = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.AddressLine1).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AddressLine2Valid(UpdateCustomerDetailsCommand command)
    {
        var address = command.Addresses.First();
        address.AddressLine2 = "a";
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.AddressLine2).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AddressSuburbValid(UpdateCustomerDetailsCommand command)
    {
        var address = command.Addresses.First();
        address.Suburb = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.Suburb).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AddressCityValid(UpdateCustomerDetailsCommand command)
    {
        var address = command.Addresses.First();
        address.City = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.City).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_AddressPostalCodeValid(UpdateCustomerDetailsCommand command)
    {
        var address = command.Addresses.First();
        address.PostalCode = string.Empty;
        
        var result = await _addressValidator.TestValidateAsync(address);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode).Only();
    }
    
    [Theory]
    [ClassData(typeof(UpdateCustomerDetailsTheoryData))]
    public async Task UpdateCustomerDetailsCommand_SuccessfulUpdate(UpdateCustomerDetailsCommand command)
    {
        // Get customer from the in memory db as we need actual data to update
        var customerToUpdate = await _sharedTestFixture.BookStoreDbContext.Customers
            .FirstAsync(CancellationToken.None);
        
        // Create a new command as we cant use all the generated data from the param
        var updateCommand = new UpdateCustomerDetailsCommand
        {
            Id = customerToUpdate.Id,
            FirstName = command.FirstName,
            Surname = command.Surname,
            Addresses = _mapper.Map<IList<AddressVm>>(customerToUpdate.Addresses)
        };
        
        // Edit an address
        var editMe = updateCommand.Addresses.First();
        var info = command.Addresses.First();

        editMe.AddressLine1 = info.AddressLine1;
        editMe.AddressLine2 = info.AddressLine2;
        editMe.Suburb = info.Suburb;
        editMe.City = info.City;
        editMe.PostalCode = info.PostalCode;
        editMe.IsPrimary = info.IsPrimary;
        
        // Add a new Address
        updateCommand.Addresses.Add(new AddressVm
        {
            AddressLine1 = "123 SomeStreet",
            AddressLine2 = "Some Complex",
            Suburb = "Suburbia",
            City = "Some City",
            IsPrimary = false,
            PostalCode = "1234"
        });

        // Create the handler
        var generatePdfCommandHandler = new UpdateCustomerDetailsCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);

        // Perform the update operation
        await generatePdfCommandHandler.Handle(updateCommand, CancellationToken.None);
        
        // Query the in memory db for the record that should have been updated above.
        var updatedCustomer = await _sharedTestFixture.BookStoreDbContext.Customers
            .FirstAsync(x => x.Id == updateCommand.Id, CancellationToken.None);
        
        // Ensure that what was created is what we sent in via the command
        updatedCustomer.FirstName.ShouldMatch(updateCommand.FirstName);
        updatedCustomer.Surname.ShouldMatch(updateCommand.Surname);

        // foreach (var address in command.Addresses)
        // {
        //     // There is probably a better way to test that addresses were created, but you get the point.
        //     updatedCustomer.Addresses.FirstOrDefault(x => x.AddressLine1 == address.AddressLine1).ShouldNotBeNull();
        //     updatedCustomer.Addresses.FirstOrDefault(x => x.Suburb == address.Suburb).ShouldNotBeNull();
        //     updatedCustomer.Addresses.FirstOrDefault(x => x.City == address.City).ShouldNotBeNull();
        //     updatedCustomer.Addresses.FirstOrDefault(x => x.PostalCode == address.PostalCode).ShouldNotBeNull();
        // }
    }
}