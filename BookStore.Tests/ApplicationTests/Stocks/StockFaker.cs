using System.Collections.Generic;
using Bogus;
using BookStore.Domain.Entities;

namespace BookStore.Tests.ApplicationTests.Stocks;

public static class StockFaker
{
    private static Faker<Stock> GetStockFakerGenerator()
    {
        var faker = new Faker<Stock>()
            .RuleFor(x => x.Quantity, f => f.Random.Int(0, 300))
            .RuleFor(x => x.CostPrice, f => f.Random.Decimal(1.0m, 5000.0m))
            .RuleFor(x => x.SellingPrice, (f, instance) => f.Random.Decimal(instance.CostPrice, 5000.0m));
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Stock> CreateStocks(int count) => GetStockFakerGenerator().Generate(count);
    }
}