using MassTransit;
using Share.Contract.Events;
using Share.Contract.Messages;

namespace OrderService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        InstanceState(c => c.CurrentState);

        ConfigureCorrelationIds();

        Initially(
            When(OrderStarted)
            .Then(x =>
            {
                x.Saga.OrderId = x.Message.OrderId;
                x.Saga.DateTime = DateTime.UtcNow;
                x.Saga.Price = x.Message.Price;
                x.Saga.UserId = x.Message.UserId;
            })
            .Then(x => logger.LogInformation($"Order {x.Saga.OrderId} started"))
            .TransitionTo(Submitted)
            .Publish(context => new OrderSubmitted(context.Saga.CorrelationId)
            {
                OrderId = context.Saga.OrderId,
                Price = context.Saga.Price,
                UserId = context.Saga.UserId
            }));

        During(Submitted,
             When(OrderAccepted)
             .Then(x => logger.LogInformation($"Order {x.Saga.OrderId} accepted"))
             .TransitionTo(Completed)
             .Publish(context => new OrderCompleted(context.Saga.CorrelationId)
             {
                 OrderId = context.Saga.OrderId
             }));
            //.Finalize()); // set current state to final

        During(Submitted,
               When(OrderRejected)
               .Then(x => logger.LogInformation($"Order {x.Saga.OrderId} rejected! because {x.Message.Reason}"))
               .TransitionTo(Rejected));
        //.Finalize()); //set current state to final

        // Sets the state machine instance to Completed when in the final state. The saga
        //     repository removes completed state machine instances.
        // SetCompletedWhenFinalized();

        
    }

    public State Started { get; private set; } = default!;
    public State Submitted { get; private set; } = default!;
    public State Accepted { get; private set; } = default!;
    public State Completed { get; private set; } = default!;
    public State Rejected { get; private set; } = default!;
    public Event<IOrderStarted> OrderStarted { get; set; } = default!;
    public Event<IOrderSubmitted> OrderSubmitted { get; set; } = default!;
    public Event<IOrderAccepted> OrderAccepted { get; set; } = default!;
    public Event<IOrderCompleted> OrderCompleted { get; set; } = default!;
    public Event<IOrderRejected> OrderRejected { get; set; } = default!;

    private void ConfigureCorrelationIds()
    {
        Event(() => OrderStarted, x => x.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderSubmitted, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderAccepted, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderRejected, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderCompleted, x => x.CorrelateById(x => x.Message.CorrelationId));
    }
}
