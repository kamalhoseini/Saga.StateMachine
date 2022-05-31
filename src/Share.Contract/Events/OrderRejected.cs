using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderRejected : IOrderRejected
{
    public OrderRejected(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; init; }

    public Guid OrderId { get; }

    public string? Reason { get; }
}
