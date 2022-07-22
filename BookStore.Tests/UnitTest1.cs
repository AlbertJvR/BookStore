using BookStore.Tests.ApplicationTests.Orders;
using BookStore.Tests.Common;
using Xunit;

namespace BookStore.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var testHelper = new TestHelper();

        var dbContext = testHelper.BookStoreDbContext;

        var orders = OrderFaker.Factory.CreateOrders(10);
        dbContext.Orders.AddRange(orders);
        dbContext.SaveChanges();
    }
}