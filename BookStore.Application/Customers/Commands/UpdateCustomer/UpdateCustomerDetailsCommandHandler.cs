using AutoMapper;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Customers.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerDetailsCommand, CustomerVm>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<CustomerVm> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
    {
        // TODO: simplify the validation exception stuff, it looks gross
        var customerToUpdate = await _context.Customers
                                   .FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new ValidationException(
                                   new List<ValidationFailure> 
                                       { new ValidationFailure("Id", "No customer with the provided Id exists.") });

        customerToUpdate.FirstName = request.FirstName;
        customerToUpdate.Surname = request.Surname;

        foreach (var address in request.Addresses)
        {
            var addressToUpdate = customerToUpdate.Addresses.First(x => x.Id == address.Id);

            addressToUpdate.AddressLine1 = address.AddressLine1;
            addressToUpdate.AddressLine2 = address.AddressLine2;
            addressToUpdate.Suburb = address.Suburb;
            addressToUpdate.City = address.City;
            addressToUpdate.PostalCode = address.PostalCode;
            addressToUpdate.IsPrimary = address.IsPrimary;
        }
        
        _context.Customers.Update(customerToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerVm>(customerToUpdate);
    }
}