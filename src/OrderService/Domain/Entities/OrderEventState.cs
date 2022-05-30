using OrderService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Entities;

public class OrderEventState
{
    [Key]
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime DateTime { get; set; }
    public OrderState State { get; set; }

}
