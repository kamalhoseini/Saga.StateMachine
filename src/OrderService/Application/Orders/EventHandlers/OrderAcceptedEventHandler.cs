using MassTransit;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.EventHandlers;

public class OrderAcceptedEventHandler : IConsumer<IOrderAccepted>
{
    private readonly ILogger<OrderAcceptedEventHandler> _logger;
    public OrderAcceptedEventHandler(ILogger<OrderAcceptedEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IOrderAccepted> context)
    {
        _logger.LogInformation($"Order {context.Message.OrderId} accepted");
        await Task.CompletedTask;
    }
}
