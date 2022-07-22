using BookStore.Persistence;
using BookStore.Tests.ApplicationTests.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookStore.Tests.Common;

public class SharedTestFixture
{
    public SharedTestFixture()
    {
        MediatorMock = new Mock<IMediator>();
        var builder = new DbContextOptionsBuilder<BookStoreDbContext>();
        builder.UseInMemoryDatabase("BookStore");
        var dbContextOptions = builder.Options;
        BookStoreDbContext = new BookStoreDbContext(dbContextOptions);
        BookStoreDbContext.Database.EnsureDeleted();
        BookStoreDbContext.Database.EnsureCreated();
        var orders = OrderFaker.Factory.CreateOrders(15);
        BookStoreDbContext.Orders.AddRange(orders);
        BookStoreDbContext.SaveChanges();
    }
    
    public Mock<IMediator> MediatorMock { get; }
    public BookStoreDbContext BookStoreDbContext { get; private set; }
}