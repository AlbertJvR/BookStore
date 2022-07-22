using System.Linq.Expressions;
using BookStore.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookStore.Domain.Common.EFCoreConverters;

public class OrderStatusValueConverter : ValueConverter<OrderStatus, string>
{
    public OrderStatusValueConverter(ConverterMappingHints? mappingHints = null)
        : base(ConvertToProviderExpression, ConvertFromProviderExpression, mappingHints)
    {
        
    }
    new static readonly Expression<Func<OrderStatus, string>> ConvertToProviderExpression = x => MapToProviderFormat(x);
    new static readonly Expression<Func<string, OrderStatus>> ConvertFromProviderExpression = x => MapFromProviderFormat(x);

    private static string MapToProviderFormat(OrderStatus from)
    {
        return from switch
        {
            OrderStatus.Created => "Created",
            OrderStatus.Paid => "Paid",
            OrderStatus.Shipped => "Shipped",
            OrderStatus.AwaitingPayment => "AwaitingPayment",
            _ => throw new ArgumentNullException(from.ToString(), "Provided value for OrderStatus does not exist")
        };
    }
        
    private static OrderStatus MapFromProviderFormat(string from)
    {
        return from switch
        {
            "Created" => OrderStatus.Created,
            "Paid" => OrderStatus.Paid,
            "Shipped" => OrderStatus.Shipped,
            "AwaitingPayment" => OrderStatus.AwaitingPayment,
            _ => throw new ArgumentNullException(from, "Provided value for OrderStatus does not exist")
        };
    }
}