using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Saga;

namespace OrderService.Application.Interfaces;

public interface IOrderContext
{
    public DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);   
}
