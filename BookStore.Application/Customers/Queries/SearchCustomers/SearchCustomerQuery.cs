using BookStore.Application.Customers.Models;
using MediatR;

namespace BookStore.Application.Customers.Queries.SearchCustomers;

public class SearchCustomerQuery : IRequest<List<CustomerVm>>
{
    
}