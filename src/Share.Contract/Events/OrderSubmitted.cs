using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderSubmitted : IOrderSubmitted
{
    public OrderSubmitted(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; init; }

    public Guid OrderId { get; }

    public int Price { get; }

    public Guid UserId { get; }
}
