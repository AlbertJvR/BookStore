using BookStore.Application.Common.Models;
using FluentValidation;

namespace BookStore.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerDetailsCommandValidator : AbstractValidator<UpdateCustomerDetailsCommand>
{
    public UpdateCustomerDetailsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithMessage($"Please provide a valid customer {nameof(UpdateCustomerDetailsCommand.Id)}");
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(UpdateCustomerDetailsCommand.FirstName)}")
            .MinimumLength(2)
            .WithMessage($"Please provide a {nameof(UpdateCustomerDetailsCommand.FirstName)} longer than 2 characters.");
        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(UpdateCustomerDetailsCommand.Surname)}")
            .MinimumLength(2)
            .WithMessage($"Please provide a {nameof(UpdateCustomerDetailsCommand.Surname)} longer than 2 characters.");
        RuleFor(x => x.Addresses)
            .Must(x => x.Count >= 1)
            .WithMessage($"Please provide at least 1 Address");
        RuleForEach(x => x.Addresses)
            .SetValidator(new AddressValidator());

    }
}

public class AddressValidator : AbstractValidator<AddressVm>
{
    public AddressValidator()
    {
        RuleFor(x => x.AddressLine1)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(AddressVm.AddressLine1)}")
            .MinimumLength(2)
            .WithMessage($"Please provide a {nameof(AddressVm.AddressLine2)} longer than 2 characters.");
        When(x => !string.IsNullOrEmpty(x.AddressLine2), () =>
        {
            RuleFor(x => x.AddressLine2)
                .MinimumLength(2)
                .WithMessage($"Please provide a {nameof(AddressVm.AddressLine2)} longer than 2 characters.");
        });
        RuleFor(x => x.Suburb)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(AddressVm.Suburb)}")
            .MinimumLength(2)
            .WithMessage($"Please provide a {nameof(AddressVm.Suburb)} longer than 2 characters.");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(AddressVm.City)}")
            .MinimumLength(2)
            .WithMessage($"Please provide a {nameof(AddressVm.City)} longer than 2 characters.");
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(AddressVm.PostalCode)}")
            .MinimumLength(4)
            .WithMessage($"Please provide a {nameof(AddressVm.PostalCode)} longer than 4 characters.");
    }
}