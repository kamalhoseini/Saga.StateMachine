using MassTransit;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.EventHandlers;

public class OrderRejectedEventHandler : IConsumer<IOrderRejected>
{

    public OrderRejectedEventHandler()
    {
    }
    public async Task Consume(ConsumeContext<IOrderRejected> context)
    {

    }
}
