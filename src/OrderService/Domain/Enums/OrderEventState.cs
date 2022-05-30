namespace OrderService.Domain.Enums;

public enum OrderEventState: byte
{
    Submitted = 0,
    Created = 1,
    Rejected = 2
}
