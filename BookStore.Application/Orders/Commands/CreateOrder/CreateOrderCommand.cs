using BookStore.Application.Common.Models;
using MediatR;

namespace BookStore.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<long>
{
    public long CustomerId { get; set; }
    public decimal OrderTotal { get; set; }

    public ICollection<OrderItemVm> OrderItems { get; set; } = new List<OrderItemVm>();
}