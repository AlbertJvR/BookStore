namespace BookStore.Application.Common.Models;

public class CustomerVm
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public ICollection<AddressVm> Addresses { get; set; } = new List<AddressVm>();
    public ICollection<OrderVm> Orders { get; set; } = new List<OrderVm>();
}