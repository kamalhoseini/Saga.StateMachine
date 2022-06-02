using MediatR;

namespace OrderService.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<Unit>
{
    public Guid OrderId { get; set; }
    public DeleteOrderCommand()
    {

    }
    public DeleteOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }
}
