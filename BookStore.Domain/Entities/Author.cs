namespace BookStore.Domain.Entities;

public class Author
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}