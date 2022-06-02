using MassTransit;
using MediatR;
using OrderService.Application.Orders.Commands.DeleteOrder;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.EventHandlers;

public class OrderRejectedEventHandler : IConsumer<IOrderRejected>
{
    private readonly IMediator _mediator;
    public OrderRejectedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<IOrderRejected> context)
    {
        await _mediator.Send(new DeleteOrderCommand(context.Message.OrderId));
    }
}
