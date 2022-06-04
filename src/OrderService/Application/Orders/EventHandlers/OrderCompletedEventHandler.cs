using MassTransit;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.EventHandlers;

public class OrderCompletedEventHandler : IConsumer<IOrderCompleted>
{
    private readonly ILogger<OrderCompletedEventHandler> _logger;
    public OrderCompletedEventHandler(ILogger<OrderCompletedEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IOrderCompleted> context)
    {
        _logger.LogInformation($"Order {context.Message.OrderId} accepted");
        await Task.CompletedTask;
    }
}
