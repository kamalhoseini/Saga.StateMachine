using Automatonymous;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Saga;
public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State? CurrentState { get; set; }
    public Guid OrderId { get; set; }
    [Timestamp]
    public byte[]? RowVersion { get; set; }
}