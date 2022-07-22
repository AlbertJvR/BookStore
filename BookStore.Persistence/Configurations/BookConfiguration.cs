using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("book");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Title)
            .HasColumnName("title")
            .HasColumnType("text");
        builder.Property(t => t.DatePublished)
            .HasColumnName("date_published")
            .HasColumnType("timestamp");
        builder.Property(t => t.Publisher)
            .HasColumnName("publisher")
            .HasColumnType("text"); 
        builder.Property(t => t.Isdn)
            .HasColumnName("isdn")
            .HasColumnType("text");

        builder.HasOne(t => t.Author)
            .WithMany(t => t.Books)
            .HasForeignKey(t => t.AuthorId);
        
        builder.HasOne(t => t.Author)
            .WithMany(t => t.Books)
            .HasForeignKey(t => t.AuthorId);

        builder.HasOne(t => t.Stock)
            .WithOne(t => t.Book)
            .HasForeignKey<Book>(t => t.StockId);
    }
}