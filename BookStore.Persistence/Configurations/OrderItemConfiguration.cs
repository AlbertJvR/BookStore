using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("int");
        builder.Property(t => t.LineTotal)
            .HasColumnName("line_total")
            .HasColumnType("numeric");

        builder.HasOne(t => t.Order)
            .WithMany(t => t.OrderItems)
            .HasForeignKey(t => t.OrderId);
        
        builder.HasOne(t => t.Book)
            .WithMany(t => t.OrderItems)
            .HasForeignKey(t => t.BookId);
    }
}