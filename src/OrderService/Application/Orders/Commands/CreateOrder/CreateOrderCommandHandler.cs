using MassTransit;
using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using Share.Contract.Events;
using Share.Contract.Messages;

namespace OrderService.Application.Orders.Commands.CreateOrder;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
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
       (
            request.OrderId,
            request.UserId,
            request.Price,
            DateTime.UtcNow
        );
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish<IOrderStarted>(new OrderStarted()
        {
            OrderId = request.OrderId,
            UserId = request.UserId,
            Price = request.Price
        });

        return order.Id;
    }
}
