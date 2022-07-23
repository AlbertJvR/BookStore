using FluentValidation;

namespace BookStore.Application.Customers.Queries.SearchCustomers;

public class SearchCustomerQueryValidator : AbstractValidator<SearchCustomerQuery>
{
    public SearchCustomerQueryValidator()
    {
        When(x => x.Id != default, () =>
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .WithMessage($"Please provide a valid customer {nameof(SearchCustomerQuery.Id)}");
        });

        When(x => !string.IsNullOrWhiteSpace(x.FirstName), () =>
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage($"Please provide a {nameof(SearchCustomerQuery.FirstName)} longer than 2 characters.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Surname), () =>
        {
            RuleFor(x => x.Surname)
                .MinimumLength(2)
                .WithMessage($"Please provide a {nameof(SearchCustomerQuery.Surname)} longer than 2 characters.");
        });
    }
}