namespace Share.Contract.Messages;
public interface IOrderRejected
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public string? Reason { get; }
}
