using System.Collections.Generic;
using Bogus;
using BookStore.Domain.Entities;

namespace BookStore.Tests.ApplicationTests.Addresses;

public static class AddressFaker
{
    private static Faker<Address> GetAddressFakerGenerator()
    {
        var faker = new Faker<Address>()
            .RuleFor(x => x.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(x => x.AddressLine2, f => f.Random.Words(4))
            .RuleFor(x => x.Suburb, f => f.Lorem.Word())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.IsPrimary, f => f.Random.Bool());
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Address> CreateAddresses(int count) => GetAddressFakerGenerator().Generate(count);
    }
}