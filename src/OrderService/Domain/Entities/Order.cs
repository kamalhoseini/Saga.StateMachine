namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime OrderDate { get; set; }
}
