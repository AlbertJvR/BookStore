using BookStore.Application.Common.Models;
using FluentValidation;

namespace BookStore.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage($"Please provide a valid {nameof(CreateOrderCommand.CustomerId)}");
        RuleFor(x => x.OrderItems)
            .Must(x => x.Count > 0)
            .WithMessage($"Cannot create order with no {nameof(CreateOrderCommand.OrderItems)}");
        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemValidator());
    }
}

public class OrderItemValidator : AbstractValidator<OrderItemVm>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage($"Please provide a valid value for {nameof(OrderItemVm.Quantity)}");
        RuleFor(x => x.BookId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage($"Please provide a valid value for {nameof(OrderItemVm.BookId)}");
    }
}