using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderRejected : IOrderRejected
{
    public OrderRejected(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; private set; }

    public Guid OrderId { get; set; }

    public string? Reason { get; set; }
}
