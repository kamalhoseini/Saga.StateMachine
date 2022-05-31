using MediatR;

namespace OrderService.Application.Orders.Commands;

public class CreateOrderCommand:IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public int Price { get; set; }
}
