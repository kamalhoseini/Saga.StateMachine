using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderCompleted : IOrderCompleted
{
    public OrderCompleted(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; private set; }

    public Guid OrderId { get; set; }
}
