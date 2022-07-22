using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("stock");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("int");
        builder.Property(t => t.CostPrice)
            .HasColumnName("cost_price")
            .HasColumnType("numeric");
        builder.Property(t => t.SellingPrice)
            .HasColumnName("selling_price")
            .HasColumnType("numeric");

        builder.HasOne(t => t.Book)
            .WithOne(t => t.Stock)
            .HasForeignKey<Stock>(t => t.BookId);
    }
}