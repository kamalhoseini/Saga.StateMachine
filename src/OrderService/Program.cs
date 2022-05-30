using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(OrderContext).Assembly.FullName))
);

var massTransitConfig = builder.Configuration.GetSection("MassTransit");

// MassTraansit configuration
builder.Services.AddMassTransit(x =>
{
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
builder.Services.AddMassTransitHostedService();

// Add controllers
builder.Services.AddControllers();

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
