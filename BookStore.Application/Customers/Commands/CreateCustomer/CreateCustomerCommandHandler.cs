using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var newCustomer = _mapper.Map<Customer>(request);

        await _context.Customers.AddAsync(newCustomer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
         return newCustomer.Id;
    }
}