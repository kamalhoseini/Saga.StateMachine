using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderAccepted : IOrderAccepted
{
    public OrderAccepted(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; private set; }

    public Guid OrderId { get; set; }
}
