using BookStore.Application.Common.Models;
using MediatR;

namespace BookStore.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public ICollection<AddressVm> Addresses { get; set; } = new List<AddressVm>();
}