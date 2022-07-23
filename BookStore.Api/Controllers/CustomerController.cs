using System.Net;
using BookStore.Application.Common.Models;
using BookStore.Application.Customers.Commands.CreateCustomer;
using BookStore.Application.Customers.Commands.UpdateCustomer;
using BookStore.Application.Customers.Queries.SearchCustomers;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiVersion("1.0")]
public class CustomerController : BaseController
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<CustomerVm>>> SearchCustomersAsync([FromQuery] SearchCustomerQuery request)
    {
        return Ok(await Mediator.Send(request));
    }
    
    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<long>> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
    {
        var command = new CreateCustomerCommand
        {
            Addresses = request.Addresses,
            Surname = request.Surname,
            FirstName = request.FirstName
        };
        
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [MapToApiVersion("1.0")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CustomerVm>> UpdateCustomerDetailsAsync([FromBody] UpdateCustomerRequest request)
    {
        var command = new UpdateCustomerDetailsCommand
        {
            Id = request.Id,
            Addresses = request.Addresses,
            Surname = request.Surname,
            FirstName = request.FirstName
        };
        
        return Ok(await Mediator.Send(command));
    }
}