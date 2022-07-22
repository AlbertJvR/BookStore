namespace BookStore.Application.Customers.Models;

public class OrderItemVm
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}