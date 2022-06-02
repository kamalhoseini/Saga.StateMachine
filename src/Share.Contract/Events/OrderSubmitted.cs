using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderSubmitted : IOrderSubmitted
{
    public OrderSubmitted(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get;}

    public Guid OrderId { get; set; }

    public int Price { get; set; }

    public Guid UserId { get; set; }
}
