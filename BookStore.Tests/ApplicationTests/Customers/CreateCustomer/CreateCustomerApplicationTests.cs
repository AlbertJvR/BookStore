using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Common.Behaviours;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Models;
using BookStore.Application.Customers.Commands.CreateCustomer;
using BookStore.Tests.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace BookStore.Tests.ApplicationTests.Customers.CreateCustomer;

[Collection(nameof(CustomerCollection))]
public class CreateCustomerApplicationTests
{
    private readonly SharedTestFixture _sharedTestFixture;
    private readonly IMapper _mapper;

    public CreateCustomerApplicationTests(SharedTestFixture sharedTestFixture)
    {
        _sharedTestFixture = sharedTestFixture ?? throw new ArgumentNullException(nameof(sharedTestFixture));
        _mapper = Mock.Of<IMapper>();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationMappingProfile());
        });
        _mapper = mockMapper.CreateMapper();
    }

    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_FirstNameValid(CreateCustomerCommand command)
    {
        command.FirstName = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid FirstName").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a FirstName longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_SurnameValid(CreateCustomerCommand command)
    {
        command.Surname = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid Surname").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a Surname longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AtLeastOneAddress(CreateCustomerCommand command)
    {
        command.Addresses = new List<AddressVm>();
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide at least 1 Address").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressLine1Valid(CreateCustomerCommand command)
    {
        command.Addresses.First().AddressLine1 = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid AddressLine1").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a AddressLine1 longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressLine2Valid(CreateCustomerCommand command)
    {
        command.Addresses.First().AddressLine2 = "a";
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a AddressLine2 longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressSuburbValid(CreateCustomerCommand command)
    {
        command.Addresses.First().Suburb = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid Suburb").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a Suburb longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressCityValid(CreateCustomerCommand command)
    {
        command.Addresses.First().City = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid City").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a City longer than 2 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_AddressPostalCodeValid(CreateCustomerCommand command)
    {
        command.Addresses.First().PostalCode = string.Empty;
        
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);
        var validationBehavior = new RequestValidationBehavior<CreateCustomerCommand, long>(new List<CreateCustomerCommandValidator>
        {
            new CreateCustomerCommandValidator()
        });
        

        Task task() => validationBehavior.Handle(command, CancellationToken.None, () =>
        {
            return generatePdfCommandHandler.Handle(command, CancellationToken.None);
        });
        
        var exception = await Assert.ThrowsAsync<ValidationException>(task);

        exception.Errors.ShouldNotBeNull();
        var errors = exception.Errors.ToList();

        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a valid PostalCode").ShouldNotBeNull();
        errors.FirstOrDefault(x => x.ErrorMessage == "Please provide a PostalCode longer than 4 characters.").ShouldNotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(CreateCustomerTheoryData))]
    public async Task CreateCustomerCommand_SuccessfulCreate(CreateCustomerCommand command)
    {
        var generatePdfCommandHandler = new CreateCustomerCommandHandler(_sharedTestFixture.BookStoreDbContext, _mapper);

        var newCustomerId = await generatePdfCommandHandler.Handle(command, CancellationToken.None);
        var createdCustomer = await _sharedTestFixture.BookStoreDbContext.Customers
            .FirstAsync(x => x.Id == newCustomerId, CancellationToken.None);
        
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