namespace BookStore.Application.Common.Models;

public class OrderItemVm
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}