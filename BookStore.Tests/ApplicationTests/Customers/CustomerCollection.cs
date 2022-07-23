using BookStore.Tests.Common;
using Xunit;

namespace BookStore.Tests.ApplicationTests.Customers;

[CollectionDefinition(nameof(CustomerCollection))]
public class CustomerCollection : ICollectionFixture<SharedTestFixture>
{
}