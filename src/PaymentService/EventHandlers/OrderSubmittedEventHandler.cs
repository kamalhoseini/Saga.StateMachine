using MassTransit;
using Share.Contract.Events;
using Share.Contract.Messages;

namespace PaymentService.EventHandlers;

public class OrderSubmittedEventHandler : IConsumer<IOrderSubmitted>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderSubmittedEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    public async Task Consume(ConsumeContext<IOrderSubmitted> context)
    {
        if (context.Message.Price < 1000)
        {
            await _publishEndpoint.Publish<IOrderAccepted>(new
            {
                OrderId = context.Message.OrderId
            });
        }
        else
        {
            await _publishEndpoint.Publish<IOrderRejected>(new
            {
                OrderId = context.Message.OrderId,
                Reason = "You don't have enough money"
            });
        }
    }
}
