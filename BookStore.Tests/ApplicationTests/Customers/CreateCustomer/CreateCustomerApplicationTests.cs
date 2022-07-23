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
}