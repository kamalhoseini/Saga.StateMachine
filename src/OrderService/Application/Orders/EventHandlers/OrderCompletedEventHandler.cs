using MassTransit;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.EventHandlers;

public class OrderCompletedEventHandler : IConsumer<IOrderCompleted>
{

    public OrderCompletedEventHandler()
    {
    }
    public async Task Consume(ConsumeContext<IOrderCompleted> context)
    {

    }
}
