using System.Collections.Generic;
using System.Linq;
using Bogus;
using BookStore.Domain.Entities;
using BookStore.Tests.ApplicationTests.Authors;
using BookStore.Tests.ApplicationTests.Stocks;

namespace BookStore.Tests.ApplicationTests.Books;

public static class BookFaker
{
    private static Faker<Book> GetBookFakerGenerator()
    {
        var faker = new Faker<Book>()
            .RuleFor(x => x.Title, f => f.Random.Words(f.Random.Int(1, 5)))
            .RuleFor(x => x.DatePublished, f => f.Date.Past())
            .RuleFor(x => x.Publisher, f => f.Company.CompanyName())
            .RuleFor(x => x.Isdn, f => f.Random.String(14, 14))
            .RuleFor(x => x.Author, f => AuthorFaker.Factory.CreateAuthors(1).First())
            .RuleFor(x => x.Publisher, f => f.Company.CompanyName())
            .RuleFor(x => x.Stock, f => StockFaker.Factory.CreateStocks(1).First());
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Book> CreateBooks(int count) => GetBookFakerGenerator().Generate(count);
    }
}