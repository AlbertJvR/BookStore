using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Interfaces;

public interface IBookStoreDbContext
{
    DbSet<Address> Addresses { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Stock> Stocks { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}