namespace Share.Contract.Messages;
public interface IOrderAccepted
{
    Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
}
