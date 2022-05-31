using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderCompleted : IOrderCompleted
{
    public OrderCompleted(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; init; }

    public Guid OrderId { get; }
}
