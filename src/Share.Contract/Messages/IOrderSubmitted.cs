namespace Share.Contract.Messages;
public interface IOrderSubmitted
{
    public Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
    public int Price { get; set; }
    public Guid UserId { get; set; }
}
