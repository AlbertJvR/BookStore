namespace BookStore.Domain.Entities;

public class Stock
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public long BookId { get; set; }

    public Book Book { get; set; } = null!;
}