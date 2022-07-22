namespace BookStore.Domain.Common.Enums;

public enum OrderStatus
{
    Created,
    AwaitingPayment,
    Paid,
    Shipped
}