﻿using MediatR;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public int Price { get; set; }
}
