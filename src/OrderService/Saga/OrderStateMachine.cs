using Automatonymous;
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
                x.Instance.OrderId = x.Data.OrderId;
                x.Instance.DateTime = DateTime.UtcNow;
                x.Instance.Price = x.Data.Price;
                x.Instance.UserId = x.Data.UserId;
            })
            .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} submitted"))
            .TransitionTo(Started)
            .Publish(context => new OrderSubmitted(context.Instance.CorrelationId)
            {
                OrderId = context.Instance.OrderId,
                Price = context.Instance.Price,
                UserId = context.Instance.UserId
            }));

        During(Submitted,
             When(OrderAccepted)
             .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} accepted"))
             .TransitionTo(Accepted));

        During(Submitted,
               When(OrderRejected)
               .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} rejected! because {x.Data.Reason}"))
               .TransitionTo(Rejected));
        //  .Finalize());

        During(Accepted,
              When(OrderCompleted)
              .Then(x => logger.LogInformation($"Order {x.Instance.OrderId} completed"))
              .TransitionTo(Completed)
              .Finalize());

        // SetCompletedWhenFinalized();


    }
    
    public State Started { get; private set; } = default!;
    public State Submitted { get; private set; } = default!;
    public State Accepted { get; private set; } = default!;
    public State Completed { get; private set; } = default!;
    public State Rejected { get; private set; } = default!;
    public Event<IOrderStarted> OrderStarted { get; set; }
    public Event<IOrderSubmitted> OrderSubmitted { get; private set; } = default!;
    public Event<IOrderAccepted> OrderAccepted { get; private set; } = default!;
    public Event<IOrderCompleted> OrderCompleted { get; private set; } = default!;
    public Event<IOrderRejected> OrderRejected { get; private set; } = default!;

    private void ConfigureCorrelationIds()
    {
        Event(() => OrderStarted, x => x.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderSubmitted, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderAccepted, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderRejected, x => x.CorrelateById(x => x.Message.CorrelationId));
        Event(() => OrderCompleted, x => x.CorrelateById(x => x.Message.CorrelationId));
    }
}