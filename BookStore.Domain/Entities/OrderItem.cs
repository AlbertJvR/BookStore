namespace BookStore.Domain.Entities;

public class OrderItem
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
    
    public Book Book { get; set; }
    public Order Order { get; set; }
}