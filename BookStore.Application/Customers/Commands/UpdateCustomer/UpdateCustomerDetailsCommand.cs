using BookStore.Application.Customers.Models;
using MediatR;

namespace BookStore.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerDetailsCommand : IRequest<CustomerVm>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public ICollection<AddressVm> Addresses { get; set; } = new List<AddressVm>();
}