using MassTransit;
using NotificationManagement.Infrastructure;
using NotificationManagement.Service;
using RabbitMQ.domain;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<OrderConfirmedConsumer>();
        x.AddConsumer<TrackingRegisteredConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("notification-management-queue", e =>
            {
                e.ConfigureConsumer<OrderConfirmedConsumer>(context);
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "order-confirmed-routingkey";
                    x.ExchangeType = "topic";
                });
                e.ConfigureConsumer<TrackingRegisteredConsumer>(context);
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "tracking-registered-routingkey";
                    x.ExchangeType = "topic";
                });
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "tracking-updated-routingkey";
                    x.ExchangeType = "topic";
                });
            });
            cfg.Message<IOrderConfirmedEvent>(x => { x.SetEntityName("ballcom-exchange"); });
            cfg.Publish<IOrderConfirmedEvent>(x => { x.ExchangeType = "topic"; });
        });
    });
});


builder.Services.AddScoped<NotificationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
