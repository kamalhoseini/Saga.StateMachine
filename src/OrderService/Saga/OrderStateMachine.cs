using MassTransit;
using Share.Contract.Events;
using Share.Contract.Messages;

namespace OrderService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    /// <summary>
    /// When using .Publish(event) in a During() section, which does not raise an event that is tracked in the next step..!!!!!!!!!
    /// </summary>
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        InstanceState(c => c.CurrentState);

        ConfigureCorrelationIds();

        Initially(
            When(OrderStartedEvent)
            .Then(x =>
            {
                x.Saga.OrderId = x.Message.OrderId;
                x.Saga.DateTime = DateTime.UtcNow;
                x.Saga.Price = x.Message.Price;
                x.Saga.UserId = x.Message.UserId;
            })
            .TransitionTo(Submitted)
            .Publish(context => new OrderSubmitted(context.Saga.CorrelationId)
            {
                OrderId = context.Saga.OrderId,
                Price = context.Saga.Price,
                UserId = context.Saga.UserId
            }));

        During(Submitted,
             When(OrderAcceptedEvent)
               .Then(x => logger.LogInformation($"Order {x.Saga.OrderId} accepted"))
               .TransitionTo(Completed),
             When(OrderRejectedEvent)
               .Then(x => logger.LogInformation($"Order {x.Saga.OrderId} rejected! because {x.Message.Reason}"))
               .TransitionTo(Rejected)
         );
        //.Finalize()); //set current state to final

        // Sets the state machine instance to Completed when in the final state. The saga
        //     repository removes completed state machine instances.
        // SetCompletedWhenFinalized();
    }
    private void ConfigureCorrelationIds()
    {
        // Add only events that are in the When() section of the state machine !!!!!!!!!!!!!!!!!!!
        Event(() => OrderStartedEvent, x => x.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderAcceptedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderRejectedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
    }

    public State Started { get; } = default!;
    public State Submitted { get; } = default!;
    public State Accepted { get; } = default!;
    public State Completed { get; } = default!;
    public State Rejected { get; } = default!;
    public Event<IOrderStarted> OrderStartedEvent { get; } = default!;
    public Event<IOrderSubmitted> OrderSubmittedEvent { get; } = default!;
    public Event<IOrderAccepted> OrderAcceptedEvent { get; } = default!;
    public Event<IOrderRejected> OrderRejectedEvent { get; } = default!;

}