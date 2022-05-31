namespace Share.Contract;
public interface IOrderRejected
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public Guid UserId { get; }
}
