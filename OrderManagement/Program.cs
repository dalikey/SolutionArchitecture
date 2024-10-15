using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Events;
using OrderManagement.Infrastructure.Order;
using OrderManagement.Infrastructure.Product;
using OrderManagement.Infrastructure.User;
using OrderManagement.Services;
using RabbitMQ.domain;
using RabbitMQ.domain.OrderEvents;
using RabbitMQ.domain.UserEvents;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

var mySQLConnectionString = builder.Configuration.GetConnectionString("MySQLConnection");
var mySQLEventsConnectionString = builder.Configuration.GetConnectionString("MySQLEventsConnection");
var developmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (developmentEnvironment != "Development")
{
    mySQLConnectionString = mySQLConnectionString.Replace("localhost", "mysqlserver");
    mySQLEventsConnectionString = mySQLEventsConnectionString.Replace("localhost", "mysqlserver");
}

builder.Services.AddDbContext<OrderMySQLContext>(options => options.UseMySql(mySQLConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));
builder.Services.AddDbContext<EventsMySQLContext>(options => options.UseMySql(mySQLEventsConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));

var mongoDBConnectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
var client = new MongoClient(mongoDBConnectionString);
var database = client.GetDatabase("ReadOrder");
builder.Services.AddSingleton(database);
builder.Services.AddScoped<OrderMongoDBContext>();

var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<UserConsumer>();
        x.AddConsumer<PaymentConfirmedConsumer>();
        x.AddConsumer<EventOrderUpdatedConsumer>();
        x.AddConsumer<ProductInsertedConsumer>();


        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("order-management-queue", e =>
            {
                e.ConfigureConsumer<UserConsumer>(context);
                e.ConfigureConsumer<PaymentConfirmedConsumer>(context);
                e.ConfigureConsumer<EventOrderUpdatedConsumer>(context);
                e.ConfigureConsumer<ProductInsertedConsumer>(context);
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "user-updated-routingkey";
                    x.ExchangeType = "topic";
                });
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "payment-confirmed-routingkey";
                    x.ExchangeType = "topic";
                });
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "order-updated-routingkey";
                    x.ExchangeType = "topic";
                });
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "after-payment-confirmed-routingkey";
                    x.ExchangeType = "topic";
                });
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "product-inserted-routingkey";
                    x.ExchangeType = "topic";
                });


            });
            cfg.Message<IUserUpdatedEvent>(x =>
            { 
                x.SetEntityName("user-updated-event");
            });
            cfg.Publish<IUserUpdatedEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
            cfg.Message<IPaymentConfirmedEvent>(x =>
            {
                x.SetEntityName("ballcom-exchange");
            });
            cfg.Publish<IPaymentConfirmedEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
            cfg.Message<IAfterPaymentConfirmedEvent>(x =>
            {
                x.SetEntityName("ballcom-exchange");
            });
            cfg.Publish<IAfterPaymentConfirmedEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
            cfg.Message<IOrderCanceledEvent>(x =>
            {
                x.SetEntityName("order-canceled-event");
            });
            cfg.Publish<IOrderCanceledEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
            cfg.Message<IOrderConfirmedEvent>(x => { x.SetEntityName("ballcom-exchange"); });
            cfg.Publish<IOrderConfirmedEvent>(x => { x.ExchangeType = "topic"; });
            cfg.Message<IInsertedEvent>(x => { x.SetEntityName("ballcom-exchange"); });
            cfg.Publish<IInsertedEvent>(x => { x.ExchangeType = "topic"; });
        });
    });
});
#pragma warning restore ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices

// Add other services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<EventsRepository>();

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
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Order management is running!");
Console.WriteLine("Running in environment: " + app.Environment.EnvironmentName);

app.Run();
