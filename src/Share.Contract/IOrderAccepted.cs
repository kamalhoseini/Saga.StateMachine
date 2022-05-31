namespace Share.Contract;
public interface IOrderAccepted
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public Guid UserId { get; }
}
