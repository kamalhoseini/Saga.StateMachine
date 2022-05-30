using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderContext _context;
    public CreateOrderCommandHandler(IOrderContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = request.OrderId,
            ProductId = request.ProductId,
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
