using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Saga;

namespace OrderService.Infrastructure.Persistence;

public class OrderContext : SagaDbContext/*, DbContext*/, IOrderContext
{
    public OrderContext(DbContextOptions<OrderContext> options)
    : base(options)
    { }
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new OrderStateMap(); }
    }

    public DbSet<Order> Orders => Set<Order>();
   // public DbSet<OrderState> OrderStates => Set<OrderState>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //In Npgsql, you are responsible for handling concurrency
        //This means that after loading an entity instance - to change its concurrency token before saving it to the database
        //foreach (var entry in ChangeTracker.Entries<OrderState>())
        //{
        //    switch (entry.State)
        //    {
        //        case EntityState.Modified:
        //            entry.Entity.ConcurrencyToken ++;
        //            break;
        //    }
        //}
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<OrderState>()
        //            .HasKey(c => c.CorrelationId);

        //modelBuilder.Entity<OrderState>()
        //            .HasIndex(c => c.CorrelationId);

        //modelBuilder.Entity<OrderState>()
        //            .Property(p => p.ConcurrencyToken)
        //            .IsConcurrencyToken();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

}
