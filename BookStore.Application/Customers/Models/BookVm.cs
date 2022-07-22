namespace BookStore.Application.Customers.Models;

public class BookVm
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DatePublished { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public string Isdn { get; set; } = string.Empty;
    public long AuthorId { get; set; }
    public long StockId { get; set; }
}