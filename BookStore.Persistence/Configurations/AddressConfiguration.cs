using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.AddressLine1)
            .HasColumnName("address_line_1")
            .HasColumnType("text");
        builder.Property(t => t.AddressLine2)
            .HasColumnName("address_line_2")
            .HasColumnType("text");
        builder.Property(t => t.Suburb)
            .HasColumnName("suburb")
            .HasColumnType("text");
        builder.Property(t => t.City)
            .HasColumnName("city")
            .HasColumnType("text");
        builder.Property(t => t.PostalCode)
            .HasColumnName("postal_code")
            .HasColumnType("text");
        builder.Property(t => t.IsPrimary)
            .HasColumnName("is_primary")
            .HasColumnType("boolean");

        builder.HasMany(t => t.Customers)
            .WithMany(t => t.Addresses)
            .UsingEntity("CustomerAddresses");
    }
}