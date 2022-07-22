using System.Collections.Generic;
using Bogus;
using BookStore.Domain.Entities;

namespace BookStore.Tests.ApplicationTests.Authors;

public static class AuthorFaker
{
    private static Faker<Author> GetAuthorFakerGenerator()
    {
        var faker = new Faker<Author>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName());
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Author> CreateAuthors(int count) => GetAuthorFakerGenerator().Generate(count);
    }
}