using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Domain.Entities;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Orders.Queries.SearchOrders;

public class SearchOrderQueryHandler : IRequestHandler<SearchOrderQuery, List<OrderVm>>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public SearchOrderQueryHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<List<OrderVm>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Orders.AsQueryable();
        var predicate = PredicateBuilder.New<Order>(true);

        if (request.Id != default)
        {
            predicate = predicate.Or(x => x.Id == request.Id);
        }
        
        if (request.CustomerId != default)
        {
            predicate = predicate.Or(x => x.Id == request.CustomerId);
        }

        // TODO: This can be a paginated return
        return _mapper.Map<List<OrderVm>>(
            await query
                .Where(predicate)
                .ToListAsync(cancellationToken));
    }
}