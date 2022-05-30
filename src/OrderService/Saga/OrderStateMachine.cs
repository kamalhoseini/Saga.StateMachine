using Automatonymous;

namespace OrderService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {

    }
}