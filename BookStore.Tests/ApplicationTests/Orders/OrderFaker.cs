using System.Collections.Generic;
using System.Linq;
using Bogus;
using BookStore.Domain.Common.Enums;
using BookStore.Domain.Entities;
using BookStore.Tests.ApplicationTests.Customers;

namespace BookStore.Tests.ApplicationTests.Orders;

public static class OrderFaker
{
    private static Faker<Order> GetOrderFakerGenerator()
    {
        var faker = new Faker<Order>()
            .RuleFor(x => x.Customer, f => CustomerFaker.Factory.CreateCustomers(1).First())
            .RuleFor(x => x.OrderStatus, f => f.PickRandom<OrderStatus>())
            .RuleFor(x => x.OrderItems, f => OrderItemFaker.Factory.CreateOrderItems(f.Random.Int(1, 10)))
            .RuleFor(x => x.OrderTotal, (f, instance) => instance.OrderItems.Sum(i => i.LineTotal));
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Order> CreateOrders(int count) => GetOrderFakerGenerator().Generate(count);
    }
}