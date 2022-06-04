using MassTransit;

namespace OrderService.Saga;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = default!;
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public int Price {get;set;}
    public DateTime DateTime { get; set; }
    public int ConcurrencyToken { get; set; }
}