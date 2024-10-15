using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductManagement.Infrastructure;
using ProductManagement.Services;
using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.domain;
using ProductManagement.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

var mySQLConnectionString = builder.Configuration.GetConnectionString("mySQLConnection");
var developmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine("Environment: " + developmentEnvironment);

if (developmentEnvironment != "Development")
{
    mySQLConnectionString = mySQLConnectionString.Replace("localhost", "mysqlserver");
}
Console.WriteLine("MySQL Connection String: " + mySQLConnectionString);

builder.Services.AddDbContext<ProductMySQLContext>(options =>
    options.UseMySql(mySQLConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));

// RabbitMQ
var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<ProductConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("product-management-queue", e =>
            {
                e.ConfigureConsumer<ProductConsumer>(context);
                e.Bind("ballcom", x =>
                {
                    x.RoutingKey = "product-inserted-routingkey";
                    x.ExchangeType = "topic";
                });
            });
            cfg.Message<IInsertedEvent>(x =>
            {
                x.SetEntityName("product-inserted-event");
            });
            cfg.Publish<IInsertedEvent>(x =>
            {
                x.ExchangeType = "topic";
            }); 
        });
    });
});

builder.Services.AddScoped<ProductConsumer>();


// Add other services to the container
builder.Services.AddControllers();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Product Management is running!");
Console.WriteLine("Running in environment: " + app.Environment.EnvironmentName);

app.Run();
