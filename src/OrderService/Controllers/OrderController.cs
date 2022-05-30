using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands;

namespace OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("CreateOrder")]
    public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
      => Ok(await _mediator.Send(command));
}