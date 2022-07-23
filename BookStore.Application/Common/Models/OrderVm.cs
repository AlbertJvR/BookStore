using BookStore.Domain.Common.Enums;

namespace BookStore.Application.Common.Models;

public class OrderVm
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal OrderTotal { get; set; }
    public ICollection<OrderItemVm> OrderItems { get; set; } = new List<OrderItemVm>();
}