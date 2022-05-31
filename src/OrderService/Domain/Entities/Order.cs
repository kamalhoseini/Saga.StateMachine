namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Price { get; set; }
    public DateTime OrderDate { get; set; }
}
