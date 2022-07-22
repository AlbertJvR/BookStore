namespace BookStore.Domain.Entities;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DatePublished { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public string Isdn { get; set; } = string.Empty;
    public long AuthorId { get; set; }
    public long StockId { get; set; }
    
    public Author Author { get; set; } = null!;
    public Stock Stock { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}