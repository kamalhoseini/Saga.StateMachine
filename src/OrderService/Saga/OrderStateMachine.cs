using Automatonymous;
using Share.Contract.Messages;

namespace OrderService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        InstanceState(c => c.CurrentState);
        ConfigureCorrelationIds();
    // init => transit: started publish: order started
    // during accepted => transit to completed publish: order completed
    // during rejected => transit to rejected publish: order rejected

        
        //Initially(
        //       When(OrderSubmitted)
        //       .Then(x => x.Instance.OrderId = x.Data.OrderId)
        //       .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} submitted"))
        //       .ThenAsync(c => WithdrawCustomerCreditCommand(c))
        //       .TransitionTo(Submitted)
        //       );
        //During(Submitted,
        //     When(OrderAccepted)
        //     .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} accepted"))
        //     .ThenAsync(c => TakeProductCommand(c))
        //     .TransitionTo(Accepted));
        //DuringAny(
        //       When(OrderRejected)
        //       .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} rejected! because {x.Data.Reason}"))
        //       .TransitionTo(Rejected)
        //       .Finalize());
        //During(Accepted,
        //      When(OrderCompleted)
        //      .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} completed"))
        //      .TransitionTo(Completed)
        //      .Finalize());
        //SetCompletedWhenFinalized();
    }
    public State Submitted { get; private set; } = default!;
    public State Accepted { get; private set; } = default!;
    public State Completed { get; private set; } = default!;
    public State Rejected { get; private set; } = default!;
    public Event<IOrderSubmitted> OrderSubmitted { get; private set; } = default!;
    public Event<IOrderAccepted> OrderAccepted { get; private set; } = default!;
    public Event<IOrderCompleted> OrderCompleted { get; private set; } = default!;
    public Event<IOrderRejected> OrderRejected { get; private set; } = default!;

    private void ConfigureCorrelationIds()
    {
        Event(() => OrderSubmitted, x => x.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderAccepted, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderRejected, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderCompleted, x => x.CorrelateById(x => x.Message.CorrelationId));
    }
}