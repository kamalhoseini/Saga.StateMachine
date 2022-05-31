namespace Share.Contract;
public interface IOrderCompleted
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public Guid UserId { get; }
}
