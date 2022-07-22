namespace BookStore.Domain.Entities;

public class Address
{
    public long Id { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string Suburb { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}