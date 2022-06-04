namespace Share.Contract.Messages;
public interface IOrderStarted
{
    public Guid OrderId { get; set; }
    public int Price { get; set; }
    public Guid UserId { get; set; }
}
