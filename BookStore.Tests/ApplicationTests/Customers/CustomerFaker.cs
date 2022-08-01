using System.Collections.Generic;
using Bogus;
using BookStore.Domain.Entities;
using BookStore.Tests.ApplicationTests.Addresses;

namespace BookStore.Tests.ApplicationTests.Customers;

public static class CustomerFaker
{
    private static Faker<Customer> GetCustomerFakerGenerator()
    {
        var faker = new Faker<Customer>()
            .RuleFor(x => x.Id, f => f.Random.Long(1))
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.Addresses, f => AddressFaker.Factory.CreateAddresses(3));
        return faker;
    }

    public static class Factory
    {
        public static ICollection<Customer> CreateCustomers(int count) => GetCustomerFakerGenerator().Generate(count);
    }
}