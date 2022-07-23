using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Domain.Entities;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Customers.Queries.SearchCustomers;

public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, List<CustomerVm>>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public SearchCustomerQueryHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<List<CustomerVm>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsQueryable();
        var predicate = PredicateBuilder.New<Customer>(true);
        
        if (request.Id != default)
        {
            predicate = predicate.Or(x => x.Id == request.Id);
        }
        
        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            predicate = predicate.Or(x => x.FirstName.Equals(request.FirstName));
        }
        
        if (!string.IsNullOrWhiteSpace(request.Surname))
        {
            predicate = predicate.Or(x => x.Surname.Equals(request.Surname));
        }

        // TODO: This can be a paginated return
        return _mapper.Map<List<CustomerVm>>(
            await query
                .Where(predicate)
                .ToListAsync(cancellationToken));
    }
}