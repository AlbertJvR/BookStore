using BookStore.Application.Common.Models;
using MediatR;

namespace BookStore.Application.Customers.Queries.SearchCustomers;

public class SearchCustomerQuery : IRequest<List<CustomerVm>>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}