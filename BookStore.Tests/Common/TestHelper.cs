using System;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Tests.Common;

public class TestHelper
{
    private readonly BookStoreDbContext _bookStoreDbContext;

    public TestHelper()
    {
        var builder = new DbContextOptionsBuilder<BookStoreDbContext>();
        builder.UseInMemoryDatabase("BookStore");
        var dbContextOptions = builder.Options;
        _bookStoreDbContext = new BookStoreDbContext(dbContextOptions);
        _bookStoreDbContext.Database.EnsureDeleted();
        _bookStoreDbContext.Database.EnsureCreated();
    }

    public BookStoreDbContext BookStoreDbContext => _bookStoreDbContext ?? throw new Exception("DbContext should not be null");
}