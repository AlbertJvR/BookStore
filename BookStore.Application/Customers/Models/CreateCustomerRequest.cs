namespace BookStore.Application.Customers.Models;

public record CreateCustomerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public ICollection<AddressVm> Addresses { get; set; } = new List<AddressVm>();
}