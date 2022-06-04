namespace Share.Contract.Messages;
public interface IOrderCompleted
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
}
