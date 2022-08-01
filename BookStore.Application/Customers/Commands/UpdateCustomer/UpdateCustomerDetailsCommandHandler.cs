using AutoMapper;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerDetailsCommandHandler : IRequestHandler<UpdateCustomerDetailsCommand, CustomerVm>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCustomerDetailsCommandHandler(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CustomerVm> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
    {
        // TODO: simplify the validation exception stuff, it looks gross
        var customerToUpdate = await _context.Customers
                                   .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                               ?? throw new ValidationException("Id", "No customer with the provided Id exists.");

        customerToUpdate.FirstName = request.FirstName;
        customerToUpdate.Surname = request.Surname;

        foreach (var address in customerToUpdate.Addresses)
        {
            if (request.Addresses
                .Where(x => x.Id != default)
                .All(x => x.Id != address.Id))
            {
                customerToUpdate.Addresses.Remove(address);
                continue;
            }

            var updateInfo = request.Addresses.First(x => x.Id == address.Id && x.Id != default);

            address.AddressLine1 = updateInfo.AddressLine1;
            address.AddressLine2 = updateInfo.AddressLine2;
            address.Suburb = updateInfo.Suburb;
            address.City = updateInfo.City;
            address.PostalCode = updateInfo.PostalCode;
            address.IsPrimary = updateInfo.IsPrimary;
        }

        var newAddresses = request.Addresses
            .Where(x => x.Id == default)
            .ToList();

        foreach (var newAddress in newAddresses)
        {
            customerToUpdate.Addresses.Add(_mapper.Map<Address>(newAddress));
        }

        _context.Customers.Update(customerToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerVm>(customerToUpdate);
    }
}