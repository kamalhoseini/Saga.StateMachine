using MassTransit;
using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(IOrderContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = request.OrderId,
            Price = request.Price,
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish<IOrderSubmitted>(new
              {
                  OrderId = request.OrderId,
                  UserId = request.UserId,
                  Price = request.Price
              });

        return order.Id;
    }
}
