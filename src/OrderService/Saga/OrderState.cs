﻿using Automatonymous;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Saga;
public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; } = default!;
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public int Price {get;set;}
    public DateTime DateTime { get; set; }
    [Timestamp]
    public byte[]? RowVersion { get; set; }
}