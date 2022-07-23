using FluentValidation;

namespace BookStore.Application.Orders.Queries.SearchOrders;

public class SearchOrderQueryValidator : AbstractValidator<SearchOrderQuery>
{
    public SearchOrderQueryValidator()
    {
        When(x => x.Id != default, () =>
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .WithMessage($"Please provide a valid Order {nameof(SearchOrderQuery.Id)}");
        });

        When(x => x.CustomerId != default, () =>
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .WithMessage($"Please provide a valid {nameof(SearchOrderQuery.CustomerId)}");
        });
    }
}