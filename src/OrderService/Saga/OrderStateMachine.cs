using Automatonymous;
using Share.Contract;

namespace OrderService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        //InstanceState(c => c.CurrentState);

        //SetCompletedWhenFinalized();
    }

    
}