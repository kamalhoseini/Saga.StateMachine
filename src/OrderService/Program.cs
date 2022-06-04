using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Application.Orders.EventHandlers;
using OrderService.Infrastructure.Persistence;
using OrderService.Saga;
using Share.Contract.Messages;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(connectionString,
    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(OrderContext).Assembly.FullName))
);
builder.Services.AddScoped<IOrderContext>(provider => provider.GetService<OrderContext>()!);

var massTransitConfig = builder.Configuration.GetSection("MassTransit");

// MassTraansit configuration
builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<IOrderSubmitted>();

    x.AddConsumer<OrderCompletedEventHandler>();
    x.AddConsumer<OrderRejectedEventHandler>();

    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
    .EntityFrameworkRepository(repo =>
     {
         repo.ConcurrencyMode = ConcurrencyMode.Pessimistic; // / or use Pessimistic
         repo.AddDbContext<DbContext, OrderContext>((provider, builder) =>
         {
             builder.UseSqlServer(connectionString,
             optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(OrderContext).Assembly.FullName));
         });
     });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(massTransitConfig["Host"],
            h =>
            {
                h.Username(massTransitConfig["Username"]);
                h.Password(massTransitConfig["Password"]);
            }
        );
        cfg.ConfigureEndpoints(context);
    });
});

// Add controllers
builder.Services.AddControllers();

// Add mediatr for assembly
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

//Auto update database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseRouting();

app.MapControllers();

app.Run();
