﻿using Share.Contract.Messages;

namespace Share.Contract.Events;
public class OrderStarted : IOrderStarted
{
    
    public Guid OrderId { get; set; }

    public int Price { get; set; }

    public Guid UserId { get; set; }

}
