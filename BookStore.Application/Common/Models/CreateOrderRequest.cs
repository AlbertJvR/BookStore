namespace BookStore.Application.Common.Models;

public record CreateOrderRequest
{
    public long CustomerId { get; set; }
    public decimal OrderTotal { get; set; }

    public ICollection<OrderItemVm> OrderItems { get; set; } = new List<OrderItemVm>();
}