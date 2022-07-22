using BookStore.Domain.Common.Enums;

namespace BookStore.Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal OrderTotal { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}