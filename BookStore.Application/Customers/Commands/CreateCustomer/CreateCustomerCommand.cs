using BookStore.Application.Customers.Models;
using MediatR;

namespace BookStore.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public ICollection<AddressVm> Addresses { get; set; } = new List<AddressVm>();
}