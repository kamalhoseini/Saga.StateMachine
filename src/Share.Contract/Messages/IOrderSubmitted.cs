namespace Share.Contract.Messages;
public interface IOrderSubmitted
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public int Price { get; }
    public Guid UserId { get; }
}
