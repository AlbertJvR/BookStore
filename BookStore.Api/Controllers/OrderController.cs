using System.Net;
using BookStore.Application.Common.Models;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Queries.SearchOrders;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiVersion("1.0")]
public class OrderController : BaseController
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<OrderVm>>> SearchOrdersAsync([FromQuery] SearchOrderQuery request)
    {
        return Ok(await Mediator.Send(request));
    }
    
    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<long>> CreateOrderAsync([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand
        {
            CustomerId = request.CustomerId,
            OrderItems = request.OrderItems,
            OrderTotal = request.OrderTotal
        };
        
        return Ok(await Mediator.Send(command));
    }
}