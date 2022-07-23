using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Common.Enums;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = _mapper.Map<Order>(request);

        // So this is clunky, but we cannot blindly trust the price coming from the API as it can be spoofed
        // which is why we set it this side - a good alternate approach is probably a pricing lookup service?
        var bookIdsForOrder = request.OrderItems
            .Select(x => x.BookId)
            .ToList();
        var booksWithPrices = await _context.Books
            .Select(x => new
            {
                BookId = x.Id,
                SellingPrice = x.Stock.SellingPrice
            })
            .Where(x => bookIdsForOrder.Contains(x.BookId))
            .ToListAsync(cancellationToken);

        foreach (var orderItem in newOrder.OrderItems)
        {
            orderItem.LineTotal = booksWithPrices.First(x => x.BookId == orderItem.Id).SellingPrice;
        }

        newOrder.OrderStatus = OrderStatus.Created;
        newOrder.OrderTotal = request.OrderItems.Sum(x => x.LineTotal);

        await _context.Orders.AddAsync(newOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newOrder.Id;
    }
}