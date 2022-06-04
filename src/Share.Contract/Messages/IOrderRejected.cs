namespace Share.Contract.Messages;
public interface IOrderRejected
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
    public string? Reason { get; set; }
}
