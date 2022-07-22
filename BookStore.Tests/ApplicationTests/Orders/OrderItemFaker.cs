using System.Collections.Generic;
using System.Linq;
using Bogus;
using BookStore.Domain.Entities;
using BookStore.Tests.ApplicationTests.Books;

namespace BookStore.Tests.ApplicationTests.Orders;

public static class OrderItemFaker
{
    private static Faker<OrderItem> GetOrderItemFakerGenerator()
    {
        var faker = new Faker<OrderItem>()
            .RuleFor(x => x.Book, f => BookFaker.Factory.CreateBooks(1).First())
            .RuleFor(x => x.Quantity, f => f.Random.Int(0, 500))
            .RuleFor(x => x.LineTotal, (f, instance) => instance.Quantity * instance.Book.Stock.SellingPrice);
        return faker;
    }

    public static class Factory
    {
        public static ICollection<OrderItem> CreateOrderItems(int count) => GetOrderItemFakerGenerator().Generate(count);
    }
}