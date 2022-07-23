using BookStore.Application.Common.Models;
using MediatR;

namespace BookStore.Application.Orders.Queries.SearchOrders;

public class SearchOrderQuery : IRequest<List<OrderVm>>
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
}