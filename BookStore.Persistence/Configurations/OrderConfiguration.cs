using BookStore.Domain.Common.EFCoreConverters;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.OrderStatus)
            .HasColumnName("order_status")
            .HasConversion(new OrderStatusValueConverter());
        builder.Property(t => t.OrderTotal)
            .HasColumnName("order_total")
            .HasColumnType("numeric");

        builder.HasOne(t => t.Customer)
            .WithMany(t => t.Orders)
            .HasForeignKey(t => t.CustomerId);
    }
}